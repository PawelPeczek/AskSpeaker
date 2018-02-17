using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Bidirectional {
	public class QuestionEditMessage {

		public string Message {
			get;
		} = AdminRequestTypes.QuestionEdit.GetRequestString();

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

