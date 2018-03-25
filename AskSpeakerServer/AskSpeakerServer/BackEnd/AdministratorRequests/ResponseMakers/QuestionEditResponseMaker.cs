using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class QuestionEditResponseMaker : ResponseMaker<AdminRequestTypes> {
    
    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      QuestionEditRequest request = JsonConvert.DeserializeObject<QuestionEditRequest>(rawMessage);
      QuestionsUtils eventsInfoUtils = new QuestionsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.EditQuestion(request), request.RequestID);
      return response;
    }

  }
}
