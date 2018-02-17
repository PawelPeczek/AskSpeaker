using System;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Bidirectional {
	public class EventEditCreateMessage {
		
		public string Message {
			get;
			set;
		}

		public Events Event {
			get;
			set;
		}

	}
}

