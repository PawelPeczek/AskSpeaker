using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class QuestionCancelRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
			QuestionCancelRequest request = new QuestionCancelRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest (QuestionCancelRequest request) {
			request.QuestionID = ProvideValueForIDField ("QuestionID");
		}
	}
}

