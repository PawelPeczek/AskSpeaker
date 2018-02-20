using System;

namespace AskSpeakerServer.BackEnd.Messages.Prototypes {
	public abstract class ResponsePrototype : TimeRegisteredCommunicationChunkPrototype {
		public string Response {
			get;
			set;
		}

		public int ErrorCode {
			get;
			set;
		} = 0;

		public string ErrorCause {
			get;
			set;
		}
	}
}

