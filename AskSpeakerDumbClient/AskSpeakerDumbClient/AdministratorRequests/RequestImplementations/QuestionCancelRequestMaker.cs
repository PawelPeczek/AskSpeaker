using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class QuestionCancelRequestMaker : RequestWithIDFieldsMaker {

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

