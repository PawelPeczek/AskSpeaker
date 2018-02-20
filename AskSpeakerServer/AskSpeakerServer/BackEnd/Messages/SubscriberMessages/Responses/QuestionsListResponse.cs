using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Responses {
	public class QuestionsListResponse : RegisteredResponsePrototype {

		public QuestionsListResponse(){
			Response = SubscriberRequestTypes.QuestionsRequest.GetRequestString();
		}

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

