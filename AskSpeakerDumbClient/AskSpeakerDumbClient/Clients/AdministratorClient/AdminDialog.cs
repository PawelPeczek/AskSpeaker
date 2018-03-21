using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using Newtonsoft.Json;
using System.Threading;

namespace AskSpeakerDumbClient.Clients.AdministratorClient {
	public class AdminDialog : GeneralDialog<AdminRequestTypes> {

		public override void StartDialog(){
			Credentials credentials = ProceedCredentialsDialog ();
			ManualResetEvent syncro = new ManualResetEvent (false);
			Client = new SimpleAdmin (credentials, syncro);
			Client.Open ();
			syncro.WaitOne ();
			if(!Client.IsClientConnected())
				throw new ApplicationException("Could not connect.");
			StartUserDialogLoop ();
			Client.Close ();
		}

		private Credentials ProceedCredentialsDialog(){
			Credentials result = new Credentials ();
			Console.Write ("[Login] ");
			result.Login = Console.ReadLine ();
			Console.Write ("[Password] ");
			result.Password = ReadMaskedPassword ();
			Console.WriteLine ();
			return result;
		}
			
		protected override string ExecuteUserCommand (AdminRequestTypes choosenType) {
			SubscriberRequestHandler requestHandler = new SubscriberRequestHandler (choosenType, this);
			BaseRequest request = requestHandler.PrepareRequest ();
			Console.WriteLine ("Request:");
			string result = JsonConvert.SerializeObject (request);
			Console.WriteLine (result);
			return result;
		}
	}
}

