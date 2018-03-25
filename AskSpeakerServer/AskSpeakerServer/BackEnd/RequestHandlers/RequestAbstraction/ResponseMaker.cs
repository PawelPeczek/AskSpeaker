using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction {
  public abstract class ResponseMaker<T> {
    
    protected void PrepareOperationResponseFromBroadcast(CommunicationChunk result, BroadcastPrototype broadcast, int requestID) {
      result.BroadcastResponse = broadcast;
      result.ResponseToSender = CommunicationChunk.PrepareResponse(requestID, broadcast);
    }

  }
}
