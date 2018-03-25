using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers {
  public class QuestionEditResponseMaker : AdminResponseMaker {
    
    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      QuestionEditRequest request = JsonConvert.DeserializeObject<QuestionEditRequest>(rawMessage);
      QuestionsUtils eventsInfoUtils = new QuestionsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.EditQuestion(request), request.RequestID);
      return response;
    }

  }
}
