using System;
using SuperSocket.WebSocket;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using System.Threading;

namespace AskSpeakerServer.BackEnd {
	public class AdministratorServer : WebSocketServer {

		public AdministratorServer() : base(){
			var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 10000;
			serverConfig.Port = 10000;
			serverConfig.TextEncoding = "utf-8";
			serverConfig.Security = "tls";
			serverConfig.Certificate = new SuperSocket.SocketBase.Config.CertificateConfig {
				FilePath =  Environment.CurrentDirectory + @"/cert.pfx",
				Password = "zse4%RDX",
				ClientCertificateRequired = false
			};
			Setup (serverConfig);
			NewSessionConnected += NewSessionHandler;
			NewMessageReceived += NewMessageHandler;
			NewDataReceived += async (session, value) => {
				await Task.Run(() => session.CloseWithHandshake(400, "Only string JSON messages allowed."));
			};

		}

		private async void NewSessionHandler(WebSocketSession session){
				try {
					ManualResetEvent synchro = new ManualResetEvent(false);
					session.Items.Add("SyncObject", synchro);
					Console.WriteLine ("NewSessionHandlerFired!");
					await Task.Run (() => ResolveCredentials (session));
					Console.WriteLine ("Credentials resolved");
					await Task.Run (() => CheckSingleSessionPerUser (session));
					Console.WriteLine ("SingleSession checked");
					synchro.Set();
					await Task.Run (() => SendEventsInformation (session));
				} catch (ApplicationException ex) {
					Task.Run (() => session.CloseWithHandshake (401, $"Invalid credentials. {ex.Message}"));
				}
		}

		private void ResolveCredentials(WebSocketSession session){
			List<KeyValuePair<object, object>> credentials =
				AdminAuthenticationModule.ResolveCredentials (session.Cookies);
			
			foreach (KeyValuePair<object, object> item in credentials) {
				session.Items.Add (item);
			}
		}

		private void SendEventsInformation(WebSocketSession session){
			session.Send (AdminRequestLogic.GetEventsInfoJSON (session.Items));
		}

		private async void NewMessageHandler(WebSocketSession session, string value) {
			try {
				await Task.Run(() => NewMessageTask(session, value));
			} catch(ApplicationException ex) {
				await Task.Run (() => session.CloseWithHandshake (400, $"JSON contract violation: {ex.Message}"));
			} catch(UnauthorizedAccessException ex) {
				await Task.Run (() => session.CloseWithHandshake (401, $"Unauthorized operation. {ex.Message}"));
			} catch (PasswordHasChangedException ex){
				await Task.Run (() => session.CloseWithHandshake (113, ex.Message));
			}
		}

		private void NewMessageTask(WebSocketSession session, string value){
			((ManualResetEvent)session.Items["SyncObject"]).WaitOne();
			Console.WriteLine ("New message!");
			AdminRequestHandler reqHandler = new AdminRequestHandler(session.Items, value);
			Console.WriteLine ("Before ProceedRequest");
			object response = reqHandler.ProceedRequest();
			DispathResponse(session, response);
		}

		private void CheckSingleSessionPerUser(WebSocketSession session){
			int counter = 0;
			foreach (WebSocketSession anotherSession in GetAllSessions()) {
				if (anotherSession.Items.ContainsKey ("UserID") &&
				    (int)anotherSession.Items ["UserID"] == (int)session.Items ["UserID"]) {
					counter++;
				}
			}
			if (counter > 1) throw new ApplicationException ("Another session for current user is active.");

		}

		private void DispathResponse(WebSocketSession session, object response){
			if (response != null) {
				foreach (WebSocketSession s in GetAllSessions()) {
					s.Send (JsonSerialize(response));
				}
			}
		}


	}
}

