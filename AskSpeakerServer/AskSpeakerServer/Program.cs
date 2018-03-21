using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.BackEnd;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SuperSocket.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace AskSpeakerServer {
	class MainClass {
		public static void Main (string[] args) {
			Server server = new Server ();
			server.Start ();
			Console.ReadKey ();
			server.Stop ();
		}
	}
}
