using System;
using WebSocketSharp;

namespace AskSpeakerDumbClient {
	class MainClass {
		public static void Main (string[] args) {
			Console.WriteLine ("Hello World!");
				using(var ws = new WebSocket ("ws://localhost:10000/ClientRequest")){
					ws.OnMessage += (sender, e) => {
						Console.WriteLine ($"Recieved message: {e.Data}");
					};
					ws.Connect ();
					Console.ReadKey ();
				}
		}
	}
}
