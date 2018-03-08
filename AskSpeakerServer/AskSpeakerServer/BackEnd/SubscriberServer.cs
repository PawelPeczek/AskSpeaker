using System;
using SuperSocket.WebSocket;
using SuperSocket.SocketBase.Config;
using System.Threading.Tasks;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;
using System.Threading;

namespace AskSpeakerServer.BackEnd {
	public class SubscriberServer : SyngnalizedServer {

		private AdministratorServer AdministratorServer = null;

		public SubscriberServer () : base () {
			ServerConfig serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 10000;
			serverConfig.Port = 11000;
			serverConfig.TextEncoding = "utf-8";
			Setup (serverConfig);
			NewSessionConnected += async (session) => {
				Console.WriteLine ("New clien session :)");
				await Task.Run(() => HandleInitialRequest(session));
			};
			NewDataReceived += async (session, value) => {
				await Task.Run(() => session.CloseWithHandshake(400, "Only string JSON messages allowed."));
			};
			NewMessageReceived += async (session, value) => {
				await Task.Run(() => HandleRequest(session, value));
			};
		}

		public override bool Start () {
			if (AdministratorServer == null)
				throw new ApplicationException ("Cannot start SubscriberServer server without providing AdministratorServer");
			return base.Start ();
		}

		public void ProvideAdministratorServer(AdministratorServer administratorServer){
			AdministratorServer = administratorServer;
		}

		public void PropagateMessage(QuestionBroadcast broadcast){
			string broadcastJSON = JsonSerialize (broadcast);
			string path = "/" + broadcast.EventHash;
			IEnumerable<WebSocketSession> selectedSessions = 
				GetSessions ((s) => s.Path == path);
			foreach (WebSocketSession session in selectedSessions) {
				session.Send (broadcastJSON);
			}
		}

		private void HandleInitialRequest(WebSocketSession session){
			try{
				Console.WriteLine ("Path: " + session.Path);
				string hash = GetHashFromPath(session.Path);
				string response = SubscriberRequestLogic.GetQuestionsJSON (hash);
				session.Send (response);
			} catch (ApplicationException ex){
				session.CloseWithHandshake (400, ex.Message);
			} catch(KeyNotFoundException ex){
				//session.CloseWithHandshake (404, ex.Message);
			}
		}

		private void HandleRequest(WebSocketSession session, string message){
			try {
				PreProcessedSubscriberMessage prepMessage = new PreProcessedSubscriberMessage (message);
				SubscriberRequestDispather dispather = 
					new SubscriberRequestDispather (prepMessage, GetHashFromPath (session.Path));
				CommunicationChunk response = dispather.Dispath ();
				DispathResponse (session, prepMessage.RequestType, response);
			} catch(ApplicationException ex){
				session.CloseWithHandshake (400, $"JSON contract violation: {ex.Message}");
			}
		}

		private string GetHashFromPath(string path){
			return path.Substring(path.IndexOf("/") + 1, path.Length - 1);
		}

		private void DispathResponse
			(WebSocketSession session, SubscriberRequestTypes reqType, CommunicationChunk response) {
			switch (reqType) {
				case SubscriberRequestTypes.QuestionsRequest:
					session.Send (response.PlainResponse);
					break;
				case SubscriberRequestTypes.QuestionAddRequest:
				case SubscriberRequestTypes.VoteRequest:
					session.Send (JsonSerialize (response.ResponseToSender));
					PropagateMessage ((QuestionBroadcast)response.BroadcastResponse);
					break;
			}
		}
	}
}

