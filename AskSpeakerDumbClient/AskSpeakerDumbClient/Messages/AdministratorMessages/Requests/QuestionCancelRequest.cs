using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests  {
	public class QuestionCancelRequest : RequestWithQuestionID {
		public QuestionCancelRequest() {
			Request = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
	}
}

