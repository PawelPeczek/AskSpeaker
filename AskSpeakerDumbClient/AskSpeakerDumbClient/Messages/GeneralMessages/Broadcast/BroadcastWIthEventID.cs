using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast  {
	public class BroadcastWIthEventID : BroadcastPrototype {
		public int EventID {
			get;
			set;
		}
	}
}

