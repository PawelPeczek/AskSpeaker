using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;

namespace AskSpeakerServer.BackEnd.Messages.Prototypes {
	public abstract class BroadcastPrototype : TimeRegisteredCommunicationChunkPrototype {
		public string Broadcast {
			get;
			set;
		}

		public void PrepareToSend(string header = null){
			if(header != null)
				Broadcast = header;
			SetCurrentTimestamp ();
		}
	}
}

