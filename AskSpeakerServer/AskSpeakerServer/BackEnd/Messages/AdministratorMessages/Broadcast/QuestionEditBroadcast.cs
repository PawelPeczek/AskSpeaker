using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionEditBroadcast : QuestionBroadcast {

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

