using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional {
	public class QuestionEditMessage : MessagePrototype {

		public QuestionEditMessage(){
			Message = AdminRequestTypes.QuestionEdit.GetRequestString();
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

