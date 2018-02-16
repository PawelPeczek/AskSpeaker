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
			l.Add (new KeyValuePair<String, String> ("user", "DumbUser"));
			l.Add (new KeyValuePair<String, String> ("pw", "zaq1@WSX"));
			WebSocket ws = new WebSocket ("wss://localhost:10000", "", l);
			// Just for now with self-generated certificate
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => {return true;};
			ws.Opened += async (sender, e) => {
				Console.WriteLine ("Connected!");
				await Task.Run(() => ((WebSocket) sender).Send(JsonConvert.SerializeObject(new SuPermissionsCheckRequest())));
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
