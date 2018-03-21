using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionCancelBroadcast : BroadcastWIthQuestionID {

		public QuestionCancelBroadcast() {
			Broadcast = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
	
	}
}

