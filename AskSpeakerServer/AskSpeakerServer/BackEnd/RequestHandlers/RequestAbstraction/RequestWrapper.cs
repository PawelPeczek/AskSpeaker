using System;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.Extensions;

namespace AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction {
  public abstract class RequestWrapper<T> {

    protected PreProcessedMessage<T> Message;

    public abstract CommunicationChunk HandleStandardRequest();

    public abstract string HandleInitRequest();

    protected OperationResponse PrepareErrorResponse(ResponseCodes errorCode, string message) {
      OperationResponse result = new OperationResponse();
      result.ErrorCode = errorCode.GetResponseCodeInt();
      result.ErrorCause = message;
      result.PrepareToSend(Message.RequestID, Message.Request);
      return result;
    }
  }
}
