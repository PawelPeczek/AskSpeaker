using System;
using System.Threading;
using SuperSocket.WebSocket;

namespace AskSpeakerServer.BackEnd {
	public abstract class SyngnalizedServer : WebSocketServer {
		public ManualResetEvent Synchro {
			get;
			private set;
		} = new ManualResetEvent(false);
	}
}

