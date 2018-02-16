using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional {
	public class EventOpenCloseMessage {
		
		public string Message {
			get;
			set;
		}
		public int EventID {
			get;
			set;
		}
	}
}

