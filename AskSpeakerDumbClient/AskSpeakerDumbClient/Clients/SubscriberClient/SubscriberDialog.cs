using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using System.Threading;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerDumbClient.Clients.SubscriberClient {
	public class SubscriberDialog : GeneralDialog<SubscriberRequestTypes> {
		
		public SubscriberDialog () {
			RequestTypes = Enum.GetValues(typeof(SubscriberRequestTypes));
		}

		public override void StartDialog(){
			Console.WriteLine ("Starting a dialog");
			string hash = GetHashFromUser ();
			ManualResetEvent syncro = new ManualResetEvent (false);
			Client = new SimpleSubscriber (hash, syncro);
			Client.Open ();
			syncro.WaitOne ();
			if(!Client.IsClientConnected())
				throw new ApplicationException("Could not connect.");
			StartUserDialogLoop ();
			Client.Close ();
		}


		protected override string ExecuteUserCommand (SubscriberRequestTypes choosenType) {
			SubscriberRequestHandler requestHandler = new SubscriberRequestHandler (choosenType, this);
			BaseRequest request = requestHandler.PrepareRequest ();
			Console.WriteLine ("Request:");
			string result = JsonConvert.SerializeObject (request);
			Console.WriteLine (result);
			return result;
		}

		private string GetHashFromUser () {
			string hash;
			do {
				Console.WriteLine ("Insert event hash:");
				hash = Console.ReadLine ();
				if (hash.Length != 6)
					Console.WriteLine ("Valid hash length: 6 chars.");
			} while(hash.Length != 6);
			return hash;
		}
	}
}

