using System;
using AskSpeakerServer.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests {
	public class BaseRequest : RequestPrototype {
		public int RequestID {
			get;
			set;
		}
	}
}

