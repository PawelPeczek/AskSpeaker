using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class QuestionEditRequest : BaseRequest {

		public QuestionEditRequest(){
			Request = AdminRequestTypes.QuestionEdit.GetRequestString();
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

