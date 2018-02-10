using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional {
	public class EventCloseMessage {
		
		public string Message {
			get;
		} = AdminRequestTypes.EventClose.GetRequestString();

		public int EventID {
			get;
			set;
		}
	}
}

