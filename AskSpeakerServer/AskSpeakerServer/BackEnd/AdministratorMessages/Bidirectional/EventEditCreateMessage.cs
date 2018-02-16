using System;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional {
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

