using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class VoteQuestionRequest : RegisteredRequestPrototype {

		public VoteQuestionRequest(){
			Request = SubscriberRequestTypes.VoteRequest.GetRequestString();
		}

		public bool VoteUp {
			get;
			set;
		}
	}
}

