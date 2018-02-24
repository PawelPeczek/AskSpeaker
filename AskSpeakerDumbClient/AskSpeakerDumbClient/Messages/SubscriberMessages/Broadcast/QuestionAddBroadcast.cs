using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests {
	public class QuestionAddBroadcast : QuestionBroadcast {

		public QuestionAddBroadcast(){
			Broadcast =  SubscriberRequestTypes.QuestionAddRequest.GetRequestString();
		}			

		public Questions Question {
			get;
			set;
		}
	}
}

