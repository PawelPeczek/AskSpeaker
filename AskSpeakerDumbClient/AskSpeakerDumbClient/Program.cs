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
					ws.OnError += (object sender, ErrorEventArgs e) => {
						Console.WriteLine (e.Exception.Message);
					};
					ws.OnOpen += Console.WriteLine ("OnOpen()");
				Console.WriteLine (ws.ReadyState);
				Console.ReadKey ();
					ws.Connect ();
				Console.WriteLine (ws.Ping());
					Console.ReadKey ();
				}
		}
	}
}
