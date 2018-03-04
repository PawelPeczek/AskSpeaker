using System;
using WebSocket4Net;
using System.Collections.Generic;
using System.Threading;

namespace AskSpeakerDumbClient.Clients.SubscriberClient {
	public class SimpleSubscriber : GeneralClient {
		public SimpleSubscriber (ManualResetEvent syncro) {
			Syncro = syncro;
			List<KeyValuePair<String, String>> cookies = new List<KeyValuePair<String, String>> ();
			Client = new WebSocket ("wss://localhost:11000");
			SetDeaultHandlers ();
		}
	}
}

