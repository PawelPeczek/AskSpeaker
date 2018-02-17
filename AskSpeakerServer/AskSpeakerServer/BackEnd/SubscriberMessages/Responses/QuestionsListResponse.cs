using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.SubscriberMessages.Responses {
	public class QuestionsListResponse {

		public string Response {
			get;
		} = SubscriberRequestTypes.QuestionsRequest.GetRequestString();

		public string Path {
			get;
			set;
		}

		public HashSet<Questions> Questions {
			get;
			set;
		}

	}
}

