using System;
using WebSocket4Net;

namespace AskSpeakerDumbClient.Clients {
	public abstract class GeneralClient {

		protected WebSocket Client;

		protected void SimpleNewMessageNotifier(object sender, MessageReceivedEventArgs e){
			Console.WriteLine ("====================NewMessage====================");
			Console.WriteLine (e.Message);
			Console.WriteLine ("========================End=======================");
		}

		protected void SimpleErrorNotifier(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
			Console.WriteLine ("ERROR");
			Console.WriteLine (e.Exception.Message);
		}

		protected void SimpleCloseHandler(object sender, EventArgs e) {
			if(e.GetType() == typeof(ClosedEventArgs)){
				Console.WriteLine (((ClosedEventArgs)e).Code);
				Console.WriteLine (((ClosedEventArgs)e).Reason);
			} else {
				Console.WriteLine ("Could not connect!");
			}
		}

		protected void SimpleNewConnectionHandler (object sender, EventArgs e){
			Console.WriteLine ("Connected, waiting for init response!");
		}

		public void Send(string message){
			Client.Send (message);
		}

		public void Close(){
			if(Client.State != WebSocketState.Closed)
				Client.Close ();
		}

		public void Open(){
			Client.Open ();
		}			
	}
}

