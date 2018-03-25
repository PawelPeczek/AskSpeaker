using System;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers {
  public class QuestionAddRequestResponseMaker : SubscriberResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, string hash) {
      CommunicationChunk response = new CommunicationChunk();
      QuestionAddRequest request = JsonConvert.DeserializeObject<QuestionAddRequest>(rawMessage);
      QuestionsUtils questionsUtils = new QuestionsUtils(hash);
      PrepareOperationResponseFromBroadcast(response, questionsUtils.AddQuestion(request), request.RequestID);
      return response;
    }
  
  }
}
