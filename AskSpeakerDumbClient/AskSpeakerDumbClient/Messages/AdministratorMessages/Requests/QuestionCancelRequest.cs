using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests  {
	public class QuestionCancelRequest : BaseRequest {

		public QuestionCancelRequest() {
			Request = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
			
		public int QuestionID {
			get;
			set;
		}
	}
}

