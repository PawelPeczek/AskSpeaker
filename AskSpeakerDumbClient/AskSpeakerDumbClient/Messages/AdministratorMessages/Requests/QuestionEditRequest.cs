using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class QuestionEditRequest : RequestWithQuestionID {

		public QuestionEditRequest(){
			Request = AdminRequestTypes.QuestionEdit.GetRequestString();
		}

		public string NewQuestionContent {
			get;
			set;
		}
	}
}

