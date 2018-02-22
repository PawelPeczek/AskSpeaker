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

		protected void PrepareSelfDomainResult(CommunicationChunk result, BroadcastPrototype broadcast){
			result.SelfDomainMessage = broadcast;
			result.ResponseToSender = CommunicationChunk.PrepareResponse(Message.RequestID, broadcast); 
		}

		protected void PrepareMultiDomainResult(CommunicationChunk result, BroadcastPrototype broadcast){
			PrepareSelfDomainResult (result, broadcast);
			result.OtherDomainMessage = broadcast;
		}

		protected void PrepareOtherDomainResult(CommunicationChunk result, BroadcastPrototype broadcast){
			result.ResponseToSender = CommunicationChunk.PrepareResponse(Message.RequestID, broadcast); 
			result.OtherDomainMessage = broadcast;
		}

		protected void PrepareSuperAdminResult(CommunicationChunk result, BroadcastPrototype broadcast){
			result.SuperAdminMessage = broadcast;
			result.ResponseToSender = CommunicationChunk.PrepareResponse(Message.RequestID, broadcast);
		}

		protected OperationResponse PrepareErrorResponse(ResponseCodes errorCode, string message){
			OperationResponse result = new OperationResponse ();
			result.ErrorCode= errorCode.GetResponseCodeInt ();
			result.ErrorCause = message;
			result.PrepareToSend (Message.Request);
			return result;
		}
	}
}

