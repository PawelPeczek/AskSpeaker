using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class SuPermissionsCheckResponseMaker : ResponseMaker<AdminRequestTypes>{

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      SuPermissionsCheckRequest request = JsonConvert.DeserializeObject<SuPermissionsCheckRequest>(rawMessage);
      BasicDatabaseUtils basicDatabaseUtils = new BasicDatabaseUtils(credentials);
      response.ResponseToSender = basicDatabaseUtils.CheckSuPermistions(request);
      return response;
    }

  }
}
