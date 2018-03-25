using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class UserDeleteResponseMaker : ResponseMaker<AdminRequestTypes> {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      UserDeleteRequest request = JsonConvert.DeserializeObject<UserDeleteRequest>(rawMessage);
      UsersUtils basicDatabaseUtils = new UsersUtils(credentials);
      response.ResponseToSender = basicDatabaseUtils.DeactivateUser(request);
      return response;
    }

  }
}
