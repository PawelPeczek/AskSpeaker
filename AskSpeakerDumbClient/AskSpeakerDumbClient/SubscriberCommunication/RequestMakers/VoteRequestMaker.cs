using System;
using AskSpeakerDumbClient.Clients;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.SubscriberRequests.RequestMakers {
	public class VoteRequestMaker : RequestWithIDFieldsMaker<SubscriberRequestTypes>  {

		protected override BaseRequest MakeRequest () {
			VoteQuestionRequest request = new VoteQuestionRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest(VoteQuestionRequest request){
			request.QuestionID = ProvideValueForIDField ("QuestionID");
			request.VoteUp = ProceedBoolValueGettingDialog ("VoteUp");
		}

		
	}
}

