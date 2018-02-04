using System;
using WebSocketSharp.Server;

namespace AskSpeakerServer.BackEnd {
	public class ClientRequests : WebSocketBehavior {
		protected override void OnOpen () {
			Console.WriteLine ("Sending request!");
			foreach (IWebSocketSession s in Sessions.Sessions) {
				if (s != this) {
					s.Context.WebSocket.SendAsync ($"New client arrives! ID: {this.ID}", NullAction);
				}
			}
			SendAsync ("Welcome!", NullAction);
		}

		private void NullAction(bool d){
			Console.WriteLine ($"Completed with: {d}");
		}

		protected override void OnError (WebSocketSharp.ErrorEventArgs e) {
			Console.WriteLine ("Error handling");
			Console.WriteLine (e.Message);
		}

		protected override void OnClose (WebSocketSharp.CloseEventArgs e) {
			Console.WriteLine ("Closing client connection!");
		}
	}
}

