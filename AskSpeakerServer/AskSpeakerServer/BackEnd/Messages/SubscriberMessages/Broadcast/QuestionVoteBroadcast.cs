using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.SubscriberRequests;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class QuestionVoteBroadcast : BroadcastPrototype {

		public QuestionVoteBroadcast(){
			Broadcast = SubscriberRequestTypes.VoteRequest.GetRequestString();
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

