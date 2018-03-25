using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

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

