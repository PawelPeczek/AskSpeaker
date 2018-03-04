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
			ManualResetEvent syncro = new ManualResetEvent (false);
			SimpleSubscriber adminClient = new SimpleSubscriber (syncro);
			adminClient.Open ();
			syncro.WaitOne ();
			if(!adminClient.IsClientConnected())
				throw new ApplicationException("Could not connect.");
			StartUserDialogLoop ();
			adminClient.Close ();
		}


		protected override string ExecuteUserCommand (SubscriberRequestTypes choosenType) {
			SubscriberRequestHandler requestHandler = new SubscriberRequestHandler (choosenType, this);
			BaseRequest request = requestHandler.PrepareRequest ();
			Console.WriteLine ("Request:");
			string result = JsonConvert.SerializeObject (request);
			Console.WriteLine (result);
			return result;
		}
	}
}

