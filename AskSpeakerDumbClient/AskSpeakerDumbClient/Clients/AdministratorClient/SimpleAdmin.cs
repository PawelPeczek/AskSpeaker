using System;
using WebSocket4Net;
using System.Collections.Generic;
using System.Threading;

namespace AskSpeakerDumbClient.Clients.AdministratorClient {
	public class SimpleAdmin : GeneralClient {
		public SimpleAdmin (Credentials credentials, ManualResetEvent syncro) {
			Syncro = syncro;
			List<KeyValuePair<String, String>> cookies = new List<KeyValuePair<String, String>> ();
			cookies.Add (new KeyValuePair<String, String> ("user", credentials.Login));
			cookies.Add (new KeyValuePair<String, String> ("pw", credentials.Password));
			Client = new WebSocket (uri: "wss://localhost:10000", cookies: cookies);
			SetDeaultHandlers ();
		}
	}
}

