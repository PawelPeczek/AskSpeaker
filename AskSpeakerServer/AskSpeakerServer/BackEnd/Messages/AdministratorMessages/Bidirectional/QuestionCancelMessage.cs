using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional  {
	public class QuestionCancelMessage : MessagePrototype {

		public QuestionCancelMessage() {
			Message = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
			
		public int QuestionID {
			get;
			set;
		}
	}
}

