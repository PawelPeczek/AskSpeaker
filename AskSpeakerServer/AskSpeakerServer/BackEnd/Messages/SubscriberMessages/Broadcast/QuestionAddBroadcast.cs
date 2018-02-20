using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class QuestionAddBroadcast : BroadcastPrototype {

		public QuestionAddBroadcast(){
			Broadcast =  SubscriberRequestTypes.QuestionAddRequest.GetRequestString();
		}			

		public Questions Question {
			get;
			set;
		}
	}
}

