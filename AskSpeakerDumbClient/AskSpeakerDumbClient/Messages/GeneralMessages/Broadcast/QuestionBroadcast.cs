using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast {
	public abstract class QuestionBroadcast : BroadcastPrototype {
		[JsonIgnore]
		public string EventHash {
			get;
			set;
		}

		public void PrepareToSend(string eventHash, string header = null){
			EventHash = eventHash;
			if(header != null)
				Broadcast = header;
			SetCurrentTimestamp ();
		}

	}
}

