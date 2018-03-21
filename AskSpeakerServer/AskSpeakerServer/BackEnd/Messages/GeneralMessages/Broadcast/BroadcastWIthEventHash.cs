using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast  {
	public class BroadcastWIthEventHash : BroadcastPrototype {
		public string EventHash {
			get;
			set;
		}
	}
}

