using System;
using AskSpeakerServer.Extensions;

namespace AskSpeakerServer.BackEnd.Messages.Prototypes  {
	public abstract class TimeRegisteredCommunicationChunkPrototype {
		public string Timestamp {
			get;
			private set;
		}

		protected void SetCurrentTimestamp(){
			Timestamp = DateTime.Now.GetTimestamp();
		}

		public abstract void PrepareToSend(int requestID, string header);
	}
}

