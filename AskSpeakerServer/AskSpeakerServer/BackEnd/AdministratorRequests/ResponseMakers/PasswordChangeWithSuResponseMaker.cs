using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class PasswordChangeWithSuResponseMaker : ResponseMaker<AdminRequestTypes> {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      PasswordChangeSuRequest request = JsonConvert.DeserializeObject<PasswordChangeSuRequest>(rawMessage);
      UsersUtils basicDatabaseUtils = new UsersUtils(credentials);
      response.ResponseToSender = basicDatabaseUtils.ChangePasswordWithSuPermissions(request);
      return response;
    }

  }
}
