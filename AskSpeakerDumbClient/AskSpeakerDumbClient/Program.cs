using System;
using WebSocket4Net;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.EntityFramework.Entities;
using System.IO;
using AskSpeakerDumbClient.Clients.AdministratorClient;

namespace AskSpeakerDumbClient {
	class MainClass {
		public static void Main (string[] args) {

			Console.WriteLine ("Client start");
			AdminDialog dialog = new AdminDialog ();
			dialog.StartDialog ();

//			Console.WriteLine ("Hello World!");
//			List<KeyValuePair<String, String>> l = new List<KeyValuePair<String, String>> ();
//			l.Add (new KeyValuePair<String, String> ("user", "DumbUser"));
//			l.Add (new KeyValuePair<String, String> ("pw", "zaq1@WSX"));
//			WebSocket ws = new WebSocket ("ws://localhost:10000/ryr(po", "", l);
//			// Just for now with self-generated certificate
//			//ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => {return true;};
//			ws.Opened += async (sender, e) => {
//				Console.WriteLine ("Connected, waiting for init response!");
////				UserCreateRequest request = new UserCreateRequest();
////				request.UserName = "MyUser1";
////				request.Password = "zaq1@WSX";
////				UserDeleteRequest request = new UserDeleteRequest();
////				request.UserID = 2;
////				PasswordChangeRequest request = new PasswordChangeRequest();
////				request.OldPassword = "zaq1@WSX";
////				request.NewPassword = "zse4%RDX";
////				EventEditCreateMessage request = new EventEditCreateMessage();
////				request.Message = AdminRequestTypes.EventEdit.GetRequestString();
////				request.Event = new Events();
////				request.Event.EventID = 1;
////				request.Event.EventDesc = "My new modified event";
////				request.Event.EventHash = "h6k9(0";
////				request.Event.EventName = "SuperEvent 2.0";
////				request.Event.SpeakerName = "Jan";
////				request.Event.SpeakerSurname = "Kowalski";
////				EventOpenCloseMessage request = new EventOpenCloseMessage();
////				request.Message = AdminRequestTypes.EventReOpen.GetRequestString();
////				request.EventID = 1;
////				QuestionCancelMessage request = new QuestionCancelMessage();
////				PasswordChangeSuRequest request = new PasswordChangeSuRequest();
////				request.NewPassword = "zaq1@WSX";
////				request.UserID = 2;
////				UserCreateRequest request = new UserCreateRequest();
////				request.Password = "zaq1@WSX";
////				request.UserName = "kowalski";
////				UserDeleteRequest request = new UserDeleteRequest();
////				request.NewEventOwnerID = 1;
////				request.UserID = 7;
////				EventOwnershipChangeRequest request = new EventOwnershipChangeRequest();
////				request.newOwnerID = 1;
////				request.EventID = 51; 
////				await Task.Run(() => ((WebSocket) sender).Send(JsonConvert.SerializeObject(request)));
//			};
////			ws.Error += (object sender, SuperSocket.ClientEngine.ErrorEventArgs e) => {
////				Console.WriteLine (e.Exception.Message);
////				Console.WriteLine ("[ERROR]");
////			};
//
//			ws.Error += (object sender, SuperSocket.ClientEngine.ErrorEventArgs e) => {
//				Console.WriteLine ("ERROR");
//				Console.WriteLine (e.Exception.Message);
//			};
//
//
//			ws.Closed += (object sender, EventArgs e) => {
//				if(e.GetType() == typeof(ClosedEventArgs)){
//					Console.WriteLine (((ClosedEventArgs)e).Code);
//					Console.WriteLine (((ClosedEventArgs)e).Reason);
//				} else {
//					Console.WriteLine ("Could not connect!");
//				}
//			};
//			ws.MessageReceived += (object sender, MessageReceivedEventArgs e) => {
//				Console.WriteLine (e.Message);
//			};
//		
//			ws.Open ();
//			Console.ReadKey ();
//			if(ws.State != WebSocketState.Closed)
//				ws.Close ();
		}
	}
}
