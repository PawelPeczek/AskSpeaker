using System;
using AskSpeakerDumbClient.Clients;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;

namespace AskSpeakerServer.BackEnd.SubscriberRequests.RequestMakers  {
	public class QuestionAddRequestMaker : RequestMaker<SubscriberRequestTypes> {
		
		protected override BaseRequest MakeRequest () {
			QuestionAddRequest request = new QuestionAddRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest (QuestionAddRequest request) {
			request.QuestionContent = ProceedStringValueGettingDialog ("QuestionContent");
		}
	}
}

