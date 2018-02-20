using System;
using AskSpeakerServer.Extensions;

namespace AskSpeakerServer.Messages.Prototypes  {
	public class TimeRegisteredCommunicationPrototype {
		public string Timestamp {
			get;
			private set;
		}

		public void SetCurrentTimestamp(){
			Timestamp = DateTime.Now.GetTimestamp();
		}
	}
}

