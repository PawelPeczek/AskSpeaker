using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public class OperationResponse : ResponsePrototype {
		public int RequestID {
			get;
			set;
		}
			
		public void PrepareToSend(int requestID, string header = null){
			if (header != null)
				Response = header;
			SetCurrentTimestamp ();
			RequestID = requestID;
		}

		public void CopyFromBroadcast(BroadcastPrototype o){
			Response = o.Broadcast;
			Timestamp = o.Timestamp;
		}
	}
}

