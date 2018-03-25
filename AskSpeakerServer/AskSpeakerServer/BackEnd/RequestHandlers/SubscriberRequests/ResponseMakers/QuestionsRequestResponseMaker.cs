using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.RequestAbstraction;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers {
  public class QuestionsRequestResponseMaker : SubscriberResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, string hash){
      CommunicationChunk response = new CommunicationChunk();
      QuestionsRequest request = JsonConvert.DeserializeObject<QuestionsRequest>(rawMessage);
      QuestionsUtils questionsUtils = new QuestionsUtils(hash);
      response.PlainResponse = questionsUtils.GetQuestionsJSON(request);
      return response;
    }

  }
}
