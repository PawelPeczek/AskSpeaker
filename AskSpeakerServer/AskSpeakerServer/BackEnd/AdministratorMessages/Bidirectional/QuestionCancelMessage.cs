using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Bidirectional  {
	public class QuestionCancelMessage {
		
		public string Message {
			get;
		} = AdminRequestTypes.QuestionCancell.GetRequestString();

		public int QuestionID {
			get;
			set;
		}
	}
}

