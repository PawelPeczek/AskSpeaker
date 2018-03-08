using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	
	public class QuestionMergeRequestMaker: RequestWithIDFieldsMaker<AdminRequestTypes> {
		
		protected override BaseRequest MakeRequest () {
			QuestionMergeRequest request = new QuestionMergeRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest (QuestionMergeRequest request) {
			request.MasterID = ProvideValueForIDField ("MasterID");
			request.SlaveID = ProvideValueForIDField ("SlaveID");
		}
	}
}

