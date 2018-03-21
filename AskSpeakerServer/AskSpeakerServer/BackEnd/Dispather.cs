using System;
using AskSpeakerServer.BackEnd;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.Extensions;

namespace AskSpeakerServer.BackEnd {
	public abstract class Dispather {

		protected PreProcessedMessage Message;

		public abstract CommunicationChunk Dispath ();

		protected void PrepareOperationResponseFromBroadcast(CommunicationChunk result, BroadcastPrototype broadcast){
			result.BroadcastResponse = broadcast;
			result.ResponseToSender = CommunicationChunk.PrepareResponse(Message.RequestID, broadcast); 
		}


		protected OperationResponse PrepareErrorResponse(ResponseCodes errorCode, string message){
			OperationResponse result = new OperationResponse ();
			result.ErrorCode= errorCode.GetResponseCodeInt ();
			result.ErrorCause = message;
			result.PrepareToSend (Message.RequestID, Message.Request);
			return result;
		}
	}
}

