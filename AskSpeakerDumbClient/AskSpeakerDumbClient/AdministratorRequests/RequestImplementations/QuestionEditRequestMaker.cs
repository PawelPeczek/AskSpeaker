using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class QuestionEditRequestMaker : RequestWithIDFieldsMaker {

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

