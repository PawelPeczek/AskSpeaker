using System;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class VoteQuestionRequest : BaseRequest {

		public VoteQuestionRequest(){
			Request = SubscriberRequestTypes.VoteRequest.GetRequestString();
		}

		public int QuestionID {
			get;
			set;
		}

		public bool VoteUp {
			get;
			set;
		}
	}
}

