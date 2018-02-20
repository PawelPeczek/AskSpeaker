﻿using System;

namespace AskSpeakerServer.Messages.Prototypes {
	public class MessagePrototype : TimeRegisteredCommunicationPrototype {

		public string Message {
			get;
			set;
		}

		public int MessageID {
			get;
			set;
		}
	}
}

