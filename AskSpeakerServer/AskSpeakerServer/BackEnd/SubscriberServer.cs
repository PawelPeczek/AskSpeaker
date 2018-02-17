using System;
using SuperSocket.WebSocket;
using SuperSocket.SocketBase.Config;
using System.Threading.Tasks;
using AskSpeakerServer.EntityFramework;

namespace AskSpeakerServer.BackEnd {
	public class SubscriberServer : WebSocketServer {

		public SubscriberServer () : base () {
			ServerConfig serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 10000;
			serverConfig.Port = 11000;
			serverConfig.TextEncoding = "utf-8";
			Setup (serverConfig);
			NewSessionConnected += async (session) => {
				await Task.Run(() => HandleInitialRequest(session));
			};
			NewDataReceived += async (session, value) => {
				await Task.Run(() => session.CloseWithHandshake(400, "Only string JSON messages allowed."));
			};
		}


		private void HandleInitialRequest(WebSocketSession session){
			string response = SubscriberRequestLogic.GetQuestionsJSON (session.Path);
			session.Send (response);
		}
	}
}

