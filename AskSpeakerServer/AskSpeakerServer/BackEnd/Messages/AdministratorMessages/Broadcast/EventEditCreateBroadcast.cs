using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventEditCreateBroadcast : BroadcastPrototype {
		
		public Events Event {
			get;
			set;
		}

	}
}

