using System;
using WebSocket4Net;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Net;
using AskSpeakerServer.BackEnd.Messages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerDumbClient {
	class MainClass {
		public static void Main (string[] args) {
			Console.WriteLine ("Hello World!");
			List<KeyValuePair<String, String>> l = new List<KeyValuePair<String, String>> ();
			l.Add (new KeyValuePair<String, String> ("user", "MyUser"));
			l.Add (new KeyValuePair<String, String> ("pw", "zse4%RDX"));
			WebSocket ws = new WebSocket ("wss://localhost:10000", "", l);
			// Just for now with self-generated certificate
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => {return true;};
			ws.Opened += async (sender, e) => {
				Console.WriteLine ("Connected!");
//				UserCreateRequest request = new UserCreateRequest();
//				request.UserName = "MyUser1";
//				request.Password = "zaq1@WSX";
//				UserDeleteRequest request = new UserDeleteRequest();
//				request.UserID = 2;
				PasswordChangeRequest request = new PasswordChangeRequest();
				request.OldPassword = "zaq1@WSX";
				request.NewPassword = "zse4%RDX";
				await Task.Run(() => ((WebSocket) sender).Send(JsonConvert.SerializeObject(new PasswordChangeSuRequest())));
			};
			ws.Error += (object sender, SuperSocket.ClientEngine.ErrorEventArgs e) => {
				Console.WriteLine (e.Exception.Message);
				Console.WriteLine ("[ERROR]");
			};

			ws.Closed += (object sender, EventArgs e) => {
				Console.WriteLine (((ClosedEventArgs)e).Code);
				Console.WriteLine (((ClosedEventArgs)e).Reason);
			};
			ws.MessageReceived += (object sender, MessageReceivedEventArgs e) => {
				Console.WriteLine (e.Message);
			};
		
			ws.Open ();
			Console.ReadKey ();
			if(ws.State != WebSocketState.Closed)
				ws.Close ();
		}
	}
}
