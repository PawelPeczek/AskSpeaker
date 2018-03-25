using System;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers {
  public class VoteRequestResponseMaker : SubscriberResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, string hash) {
      CommunicationChunk response = new CommunicationChunk();
      VoteQuestionRequest request = JsonConvert.DeserializeObject<VoteQuestionRequest>(rawMessage);
      QuestionsUtils questionsUtils = new QuestionsUtils(hash);
      PrepareOperationResponseFromBroadcast(response, questionsUtils.VoteQuestion(request), request.RequestID);
      return response;
    }

  }
}
