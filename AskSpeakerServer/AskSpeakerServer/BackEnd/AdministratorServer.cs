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
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd {
	public class AdministratorServer : SyngnalizedServer {

		private SubscriberServer SubscriberServer = null;

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

		public void ProvideSubscriberServer(SubscriberServer subscriberServer){
			SubscriberServer = subscriberServer;
		}

		public override bool Start () {
			if (SubscriberServer == null)
				throw new ApplicationException ("Cannot start AdministratorServer server without providing SubscriberServer");
			return base.Start ();
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
			DispathResponse(session, prepMessage.RequestType , response);
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

		private void DispathResponse(WebSocketSession session, AdminRequestTypes reqType, CommunicationChunk response){
			switch (reqType) {
				case AdminRequestTypes.EventsInfoRenew:
					session.Send (response.PlainResponse);
					break;
				case AdminRequestTypes.SuPermissionsCheck:
				case AdminRequestTypes.UserCreate:
				case AdminRequestTypes.PasswordChange:
				case AdminRequestTypes.UserDelete:
					session.Send (JsonSerialize (response.ResponseToSender));
					break;
				case AdminRequestTypes.EventChangeOwnership:
					session.Send (JsonSerialize (response.ResponseToSender));
					InformNewEventOwnerIfConnected (session, (EventOwnershipChangeBroadcast)response.BroadcastResponse);
					break;
				case AdminRequestTypes.EventEdit:
				case AdminRequestTypes.EventClose:
				case AdminRequestTypes.EventCreate:
				case AdminRequestTypes.EventReOpen:
					session.Send (JsonSerialize (response.ResponseToSender));
					InformSuperAdminIfConnected (response.BroadcastResponse);
					break;
				case AdminRequestTypes.QuestionCancell:
				case AdminRequestTypes.QuestionEdit:
				case AdminRequestTypes.QuestionMerge:
					session.Send (JsonSerialize (response.ResponseToSender));
					BroadcastInfoToSubscribers ((QuestionBroadcast)response.BroadcastResponse);
					break;
				case AdminRequestTypes.PasswordChangeWithSu:
					MarkPasswdChangeIfConnected ((SuPasswdChangeResponse)response.ResponseToSender);
					session.Send (JsonSerialize (response.ResponseToSender));
					break;
			}
		}

		private void InformNewEventOwnerIfConnected(WebSocketSession session, EventOwnershipChangeBroadcast message){
			WebSocketSession targetSession =
				GetSessions ((s) => s.Items.ContainsKey ("UserID") && (int)s.Items ["UserID"] == message.newOwnerID).FirstOrDefault ();
			if (targetSession != null && targetSession != session)
				targetSession.Send (JsonSerialize (message));
		}

		private void InformSuperAdminIfConnected(BroadcastPrototype broadcast){
			IEnumerable<WebSocketSession> suSessions = 
				GetSessions ((s) => s.Items.ContainsKey ("Privilages") && (string)s.Items["Privilages"] == "SuperAdmin");
			foreach (WebSocketSession session in suSessions) {
				session.Send (JsonSerialize (broadcast));
			}
		}

		private async void BroadcastInfoToSubscribers(QuestionBroadcast broadcast){
			SubscriberServer.Synchro.WaitOne ();
			await Task.Run(() => SubscriberServer.PropagateMessage(broadcast));
		}

		private void MarkPasswdChangeIfConnected(SuPasswdChangeResponse response){
			WebSocketSession sessionChgPasswd = 
				GetSessions ((s) => s.Items.ContainsKey ("UserID") && (int)s.Items["UserID"] == response.UserID)
							.FirstOrDefault();
			if(sessionChgPasswd != null)
				sessionChgPasswd.Items ["PasswordChanged"] = true;
		}

	}
}

