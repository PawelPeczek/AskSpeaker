using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	
	public class QuestionMergeRequestMaker: RequestWithIDFieldsMaker {
		
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

