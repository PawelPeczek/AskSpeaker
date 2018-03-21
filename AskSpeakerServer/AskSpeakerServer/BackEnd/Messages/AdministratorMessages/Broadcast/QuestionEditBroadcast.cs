using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionEditBroadcast : BroadcastWIthQuestionID {

		public QuestionEditBroadcast(){
			Broadcast = AdminRequestTypes.QuestionEdit.GetRequestString();
		}

		public string NewQuestionContent {
			get;
			set;
		}
	}
}

