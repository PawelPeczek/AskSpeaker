using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionEditBroadcast : BroadcastPrototype {

		public QuestionEditBroadcast(){
			Broadcast = AdminRequestTypes.QuestionEdit.GetRequestString();
		}

		public int QuestionID {
			get;
			set;
		}

		public string NewQuestionContent {
			get;
			set;
		}
	}
}

