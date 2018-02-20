using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests{
	public class QuestionAddRequest : RegisteredRequestPrototype {

		public QuestionAddRequest(){
			Request =  SubscriberRequestTypes.QuestionAddRequest.GetRequestString();
		}			

		public string QuestionContent {
			get;
			set;
		}
	}
}

