using System;
using WebSocket4Net;
using System.Collections.Generic;

namespace AskSpeakerDumbClient.Clients.AdministratorClient {
	public class SimpleAdmin : GeneralClient {
		public SimpleAdmin (string user, string passwd) {
			List<KeyValuePair<String, String>> cookies = new List<KeyValuePair<String, String>> ();
			cookies.Add (new KeyValuePair<String, String> ("user", user));
			cookies.Add (new KeyValuePair<String, String> ("pw", passwd));
			Client = new WebSocket (uri: "wss://localhost:10000", cookies: cookies);
			Client.DataReceived += (object sender, DataReceivedEventArgs e) => {
				throw new ApplicationException("Unsupported binary data recived.");
			};
			Client.Closed += SimpleCloseHandler;
			Client.Error += SimpleErrorNotifier;
			Client.MessageReceived += SimpleNewMessageNotifier;
			Client.Opened += SimpleNewConnectionHandler;
		}
	}
}

