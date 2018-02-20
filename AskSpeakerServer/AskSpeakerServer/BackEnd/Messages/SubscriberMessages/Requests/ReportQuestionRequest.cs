using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class ReportQuestionRequest : RegisteredRequestPrototype {
		
		public ReportQuestionRequest(){
			Request = SubscriberRequestTypes.QuestionReportRequest.GetRequestString();
		}

		public int QuestionID {
			get;
			set;
		}

		public string Explanation {
			get;
			set;
		}
	}
}

