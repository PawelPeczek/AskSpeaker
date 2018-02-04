using System;
using SuperSocket.WebSocket;

namespace AskSpeakerServer.BackEnd {
	public class UserRequests : WebSocketServer {
		public UserRequests() : base(){
			var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 10000;
			serverConfig.Port = 10000;
			Setup (serverConfig);
		}

	}
}

