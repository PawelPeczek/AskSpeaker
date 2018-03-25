using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;

namespace AskSpeakerServer.BackEnd.RequestHandlers.ResponseMakers {
  public abstract class AdminResponseMaker : ResponseMaker<AdminRequestTypes> {
    
    public abstract CommunicationChunk PrepareCommunicationChunk(string rawMessage);

  }
}
