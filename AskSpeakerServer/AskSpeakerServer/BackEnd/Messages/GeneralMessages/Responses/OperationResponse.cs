using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public class OperationResponse : ResponsePrototype {
		public int RequestID {
			get;
			set;
		}

		public void PrepareToSend(int requestID, string header){
			Response = header;
			RequestID = requestID;
			SetCurrentTimestamp ();
		}

		public void CopyFromBroadcast(BroadcastPrototype o){
			RequestID = o.RequestID;
			ErrorCode = o.ErrorCode;
			ErrorCause = o.ErrorCause;
			Response = o.Broadcast;
			Timestamp - o.Timestamp;
		}
	}
}

