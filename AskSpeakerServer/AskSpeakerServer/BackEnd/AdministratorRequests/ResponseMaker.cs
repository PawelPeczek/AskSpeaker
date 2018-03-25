using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
  public abstract class ResponseMaker<T> {
    public abstract CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<Object, Object> credentials);

    protected void PrepareOperationResponseFromBroadcast(CommunicationChunk result, BroadcastPrototype broadcast, int requestID) {
      result.BroadcastResponse = broadcast;
      result.ResponseToSender = CommunicationChunk.PrepareResponse(requestID, broadcast);
    }

  }
}
