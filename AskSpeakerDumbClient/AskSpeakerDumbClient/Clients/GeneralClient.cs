using System;
using WebSocket4Net;
using System.Threading;

namespace AskSpeakerDumbClient.Clients {
	public abstract class GeneralClient {

		protected WebSocket Client;
		protected ManualResetEvent Syncro;

		public void Send(string message){
			Client.Send (message);
		}

		public void Close(){
			if(Client.State != WebSocketState.Closed)
				Client.Close ();
		}

		public void Open(){
			Console.WriteLine ("Openning connection :)");
			Client.Open ();
		}	

		public bool IsClientConnected(){
			return Client.State == WebSocketState.Open;
		}

		protected void SimpleNewMessageNotifier(object sender, MessageReceivedEventArgs e){
			Console.WriteLine ("\n====================NewMessage====================");
			Console.WriteLine (e.Message);
			Console.WriteLine ("========================End=======================");
		}

		protected void SimpleErrorNotifier(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
			Console.WriteLine ("\n====================NewMessage====================");
			Console.WriteLine ("ERROR");
			Console.WriteLine (e.Exception.Message);
			Console.WriteLine ("========================End=======================");
			Syncro.Set ();
		}

		protected void SimpleCloseHandler(object sender, EventArgs e) {
			Console.WriteLine ("SimpleCloseHandler fired!");
			if(e.GetType() == typeof(ClosedEventArgs)){
				Console.WriteLine (((ClosedEventArgs)e).Reason);
			}
		}

		protected void SimpleNewConnectionHandler (object sender, EventArgs e){
			Console.WriteLine ("Connected, waiting for init response!");
			Syncro.Set ();
		}

		protected void SetDeaultHandlers(){
			Client.DataReceived += (object sender, DataReceivedEventArgs e) => {
				throw new ApplicationException("Unsupported binary data recived.");
			};
			Client.Closed += SimpleCloseHandler;
			Client.Error += SimpleErrorNotifier;
			Client.MessageReceived += SimpleNewMessageNotifier;
			Client.Opened += SimpleNewConnectionHandler;
		}
	}
}

