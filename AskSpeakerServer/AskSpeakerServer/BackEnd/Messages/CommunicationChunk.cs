using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages {
	public class CommunicationChunk {

		public string PlainResponse {
			get;
			set;
		} = null;

		public ResponsePrototype ResponseToSender {
			get;
			set;
		} = null;

		public BroadcastPrototype BroadcastResponse {
			get;
			set;
		} = null;

		public static OperationResponse PrepareResponse(int requestID, BroadcastPrototype message) {
			OperationResponse result = new OperationResponse ();
			result.RequestID = requestID;
			result.CopyFromBroadcast (message);
			return result;
		}
	}
}

