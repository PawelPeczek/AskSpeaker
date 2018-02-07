using System;
using SuperSocket.WebSocket;
using System.IO;

namespace AskSpeakerServer.BackEnd {
	public class UserRequests : WebSocketServer {
		public UserRequests() : base(){
			var serverConfig = new SuperSocket.SocketBase.Config.ServerConfig ();
			serverConfig.MaxConnectionNumber = 10000;
			serverConfig.Port = 10000;
			serverConfig.Security = "tls";

			serverConfig.Certificate = new SuperSocket.SocketBase.Config.CertificateConfig {
				FilePath =  Environment.CurrentDirectory + @"/cert.pfx",
				Password = "zse4%RDX",
				ClientCertificateRequired = false
			};

			Setup (serverConfig);
		}

	}
}

