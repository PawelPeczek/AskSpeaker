using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests {
	public abstract class RequestWithQuestionID : BaseRequest {
		public int QuestionID {
			get;
			set;
		}
	}
}

