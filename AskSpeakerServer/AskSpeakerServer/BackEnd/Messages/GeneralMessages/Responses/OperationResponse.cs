using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public class OperationResponse : ResponsePrototype {
		public int RequestID {
			get;
			set;
		}
			
		public void PrepareToSend(string header, int requestID){
			PrepareToSend (header);
			RequestID = requestID;
		}

		public override void PrepareToSend(string header = null){
			if (header != null)
				Response = header;
			SetCurrentTimestamp ();
		}

		public void CopyFromBroadcast(BroadcastPrototype o){
			Response = o.Broadcast;
			Timestamp = o.Timestamp;
		}
	}
}

