using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional {
	public class EventEditCreateMessage : MessagePrototype {
		
		public Events Event {
			get;
			set;
		}

	}
}

