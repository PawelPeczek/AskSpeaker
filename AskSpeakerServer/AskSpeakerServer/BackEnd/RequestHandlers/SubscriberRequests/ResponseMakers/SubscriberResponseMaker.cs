using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers {
  public abstract class SubscriberResponseMaker : ResponseMaker<SubscriberRequestTypes> {

    public abstract CommunicationChunk PrepareCommunicationChunk(string rawMessage, string hash);

  }
}
