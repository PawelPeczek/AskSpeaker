using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.Extensions;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;

namespace AskSpeakerServer.BackEnd.Messages.Prototypes {
	public abstract class BroadcastPrototype : TimeRegisteredCommunicationChunkPrototype {
		public string Broadcast {
			get;
			set;
		}

		[JsonIgnore]
		public int ErrorCode {
			get;
		} = 0;

		[JsonIgnore]
		public string ErrorCause {
			get;
		}

		[JsonIgnore]
		public int RequestID {
			get;
		}

		public void PrepareToSend(int requestID, string header = null){
			if(header != null)
				Broadcast = header;
			RequestID = requestID;
			SetCurrentTimestamp ();
		}
			

		public void SetError(ResponseCodes errorCode, string errorCause){
			ErrorCode = errorCode.GetResponseCodeInt();
			ErrorCause = errorCause;
		}
	}
}

