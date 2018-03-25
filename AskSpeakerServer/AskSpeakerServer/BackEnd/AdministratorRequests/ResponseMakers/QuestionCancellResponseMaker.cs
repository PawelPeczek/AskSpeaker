using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class QuestionCancellResponseMaker : ResponseMaker<AdminRequestTypes> {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<Object, Object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      QuestionCancelRequest request = JsonConvert.DeserializeObject<QuestionCancelRequest>(rawMessage);
      QuestionsUtils eventsInfoUtils = new QuestionsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.CancellQuestion(request), request.RequestID);
      return response;
    }

  }
}
