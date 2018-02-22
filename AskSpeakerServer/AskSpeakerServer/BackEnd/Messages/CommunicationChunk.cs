using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages {
	public class CommunicationChunk {

		public object PlainResponse {
			get;
			set;
		} = null;

		public ResponsePrototype ResponseToSender {
			get;
			set;
		} = null;

		public BroadcastPrototype SelfDomainMessage {
			get;
			set;
		} = null;

		public BroadcastPrototype OtherDomainMessage {
			get;
			set;
		}

		public BroadcastPrototype SuperAdminMessage {
			get;
			set;
		}

		public static OperationResponse PrepareResponse(int requestID, BroadcastPrototype message) {
			OperationResponse result = new OperationResponse ();
			result.RequestID = requestID;
			result.CopyFromBroadcast (message);
			return result;
		}
	}
}

