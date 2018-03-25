using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests  {
	public class QuestionCancelRequest : RequestWithQuestionID {
		public QuestionCancelRequest() {
			Request = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
	}
}

