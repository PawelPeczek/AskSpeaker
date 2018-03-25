using System;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests{
	public class QuestionAddRequest : BaseRequest {

		public QuestionAddRequest(){
			Request =  SubscriberRequestTypes.QuestionAddRequest.GetRequestString();
		}			

		public string QuestionContent {
			get;
			set;
		}
	}
}

