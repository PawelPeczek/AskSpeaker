using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class SuPermissionsCheckRequestMaker : RequestMaker<AdminRequestTypes> {
		
		protected override BaseRequest MakeRequest () {
			return new SuPermissionsCheckRequest ();
 		}

		protected override void PrintMethodDialogHeader(){
			Console.WriteLine ($"{GetType ().Name} does not require configuration. [ALL DONE]");
		}
	}
}

