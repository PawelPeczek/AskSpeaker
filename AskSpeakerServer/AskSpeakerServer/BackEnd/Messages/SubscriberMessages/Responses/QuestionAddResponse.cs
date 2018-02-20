using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;

namespace AskSpeakerServer.BackEnd.SubscriberMessages.Responses {
	public class QuestionAddResponse {

		public string Response {
			get;
		} = SubscriberRequestTypes.QuestionAddRequest.GetRequestString();

		public bool Status {
			get;
			set;
		}
	}
}

