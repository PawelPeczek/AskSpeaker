using System;
using AskSpeakerServer.Extensions;

namespace AskSpeakerServer.BackEnd.Messages.Prototypes  {
	public abstract class TimeRegisteredCommunicationChunkPrototype {
		public string Timestamp {
			get;
			protected set;
		}

		protected void SetCurrentTimestamp(){
			Timestamp = DateTime.Now.GetTimestamp();
		}

		public abstract void PrepareToSend(string header);
	}
}

