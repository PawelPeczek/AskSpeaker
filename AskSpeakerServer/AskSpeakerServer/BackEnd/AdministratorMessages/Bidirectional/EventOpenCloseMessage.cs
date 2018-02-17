using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Bidirectional {
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

