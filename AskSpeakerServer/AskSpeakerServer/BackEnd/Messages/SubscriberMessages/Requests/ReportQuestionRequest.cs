using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class ReportQuestionRequest : BaseRequest {
		
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

