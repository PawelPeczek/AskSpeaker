using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional  {
	public class QuestionCancellMessage {
		
		public string Message {
			get;
		} = AdminRequestTypes.QuestionCancell.GetRequestString();

		public int QuestionID {
			get;
			set;
		}
	}
}

