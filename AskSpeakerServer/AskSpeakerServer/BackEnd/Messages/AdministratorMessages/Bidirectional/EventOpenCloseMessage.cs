using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional {
	public class EventOpenCloseMessage : MessagePrototype {

		public int EventID {
			get;
			set;
		}
	}
}

