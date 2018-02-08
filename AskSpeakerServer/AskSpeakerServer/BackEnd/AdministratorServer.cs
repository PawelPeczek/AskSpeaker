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
				await Task.Run (() => ResolveCredentials (session));
				Console.WriteLine ("Credentials resolved");
				await Task.Run (() => SendEventsInformation(session));
			} catch(ApplicationException){
				await Task.Run (() => session.CloseWithHandshake (401, "Invalid credentials."));
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
			string response = AdminRequestLogic.GetJsonEventsInfo (session.Items);
			session.Send (response);
		}

		private async void NewMessageHandler(WebSocketSession session, string value) {
			try {
				AdminRequestHandler reqHandler = new AdminRequestHandler(session.Items, value);
				string response = await Task.Run(() => reqHandler.ProceedRequest());
				await Task.Run(() => DispathResponse(session, response));
			} catch(ApplicationException ex) {
				await Task.Run (() => session.CloseWithHandshake (400, $"JSON contract violation: {ex.Message}"));
			}
		}

		private void DispathResponse(WebSocketSession session, string response){
			if (response != null) {
				foreach (WebSocketSession s in GetAllSessions()) {
					if (!s.Equals (session))
						s.Send (response);
				}
			}
		}


	}
}

