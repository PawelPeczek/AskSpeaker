using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOpenCloseBroadcast : BroadcastPrototype {
		public int EventID {
			get;
			set;
		}

	}
}

