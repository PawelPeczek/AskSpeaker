using System;
using WebSocket4Net;
using System.Collections.Generic;
using System.Threading;

namespace AskSpeakerDumbClient.Clients.SubscriberClient {
	public class SimpleSubscriber : GeneralClient {
		public SimpleSubscriber (string path, ManualResetEvent syncro) {
			Syncro = syncro;
			Client = new WebSocket ($"ws://localhost:11000/{path}");
			SetDeaultHandlers ();
		}
	}
}

