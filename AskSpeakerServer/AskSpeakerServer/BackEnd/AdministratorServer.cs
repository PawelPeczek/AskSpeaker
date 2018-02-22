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
using SuperSocket.SocketBase.Config;
using AskSpeakerServer.BackEnd.Messages;

namespace AskSpeakerServer.BackEnd {
	public class AdministratorServer : WebSocketServer {

		public AdministratorServer() : base(){
			ServerConfig serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 50;
			serverConfig.Port = 10000;
			serverConfig.TextEncoding = "utf-8";
			serverConfig.Security = "tls";
			serverConfig.Certificate = new SuperSocket.SocketBase.Config.CertificateConfig {
				FilePath =  Environment.CurrentDirectory + @"/cert.pfx",
				Password = "zse4%RDX",
				ClientCertificateRequired = false
			};
			Setup (serverConfig);
			NewSessionConnected += async (session) => {
				await Task.Run(() => HandleNewSession(session));
			};
			NewMessageReceived += async (session, value) => {
				await Task.Run(() => HandleNewMessage(session, value));
			};
			NewDataReceived += async (session, value) => {
				await Task.Run(() => session.CloseWithHandshake(400, "Only string JSON messages allowed."));
			};

		}

		private void HandleNewSession(WebSocketSession session){
				try {
					ManualResetEvent synchro = new ManualResetEvent(false);
					session.Items.Add("SyncObject", synchro);
					Console.WriteLine ("NewSessionHandlerFired!");
					ResolveCredentials (session);
					Console.WriteLine ("Credentials resolved");
					CheckSingleSessionPerUser (session);
					Console.WriteLine ("SingleSession checked");
					synchro.Set();
					SendEventsInformation (session);
				} catch (ApplicationException ex) {
					session.CloseWithHandshake (401, $"Error while authorization. {ex.Message}");
				} catch (UnauthorizedAccessException ex){
					session.CloseWithHandshake (401, $"Invalid credentials. {ex.Message}");
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

		private void HandleNewMessage(WebSocketSession session, string message) {
			try {
				Console.WriteLine (message);
				GenerateResponse(session, message);
			} catch(ApplicationException ex) {
				session.CloseWithHandshake (400, $"JSON contract violation: {ex.Message}");
			} catch(UnauthorizedAccessException ex) {
				session.CloseWithHandshake (401, $"Unauthorized operation. {ex.Message}");
			} catch (PasswordHasChangedException ex){
				session.CloseWithHandshake (113, ex.Message);
			}
		}

		private void GenerateResponse(WebSocketSession session, string message){
			((ManualResetEvent)session.Items["SyncObject"]).WaitOne();
			Console.WriteLine ("New message!");
			PreProcessedAdminMessage prepMessage = new PreProcessedAdminMessage (message);
			AdminRequestDispather dispather = new AdminRequestDispather (prepMessage, session.Items);
			CommunicationChunk response = dispather.Dispath ();
			DispathResponse(session, response);
		}

		private void CheckSingleSessionPerUser(WebSocketSession session){
			int counter = 0;
			Console.WriteLine ($"CheckSingleSessionPerUser(), sessionCount: {GetAllSessions().Count()}");
			foreach (WebSocketSession anotherSession in GetAllSessions()) {
				Console.WriteLine ($"Checking for session {session.SessionID} -> compare with {anotherSession.SessionID}");
				if (session != anotherSession &&
					anotherSession.Items.ContainsKey ("UserID") &&
				    (int)anotherSession.Items ["UserID"] == (int)session.Items ["UserID"]) {
					counter++;
				}
			}
			if (counter > 0) {
				Console.WriteLine ("Found another session with the same user!");
				throw new ApplicationException ("Another session for current user is active.");
			}

		}

		private void DispathResponse(WebSocketSession session, CommunicationChunk response){
			if (response != null) {
				string serializedResponse = JsonSerialize (response);
				bool currentSessionAtList = false;
				foreach (WebSocketSession s in GetAllSessions()) {
					Console.WriteLine ($"Sending response to {s.SessionID}");
					if (s == session)
						currentSessionAtList = true;
					s.Send (serializedResponse);
				}
				// The strange thing is that GetAllSessions() do not always
				// contains current session...
				if (!currentSessionAtList)
					session.Send (serializedResponse);
			}
		}


	}
}

