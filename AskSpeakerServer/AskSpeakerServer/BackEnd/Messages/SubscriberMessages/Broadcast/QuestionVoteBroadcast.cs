using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class QuestionVoteBroadcast : QuestionBroadcast {

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

