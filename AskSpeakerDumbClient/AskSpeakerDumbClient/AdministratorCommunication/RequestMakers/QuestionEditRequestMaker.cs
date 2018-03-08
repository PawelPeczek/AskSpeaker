using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class QuestionEditRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
			QuestionEditRequest request = new QuestionEditRequest ();
			FulfillRequest (request);
			return request;
		}
		
		private void FulfillRequest (QuestionEditRequest request) {
			request.QuestionID = ProvideValueForIDField ("QuestionID");
			request.NewQuestionContent = ProceedStringValueGettingDialog ("NewQuestionContent");
		}
	}
}

