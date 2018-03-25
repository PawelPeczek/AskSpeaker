using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers {
  public class PasswordChangeResponseMaker : AdminResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      PasswordChangeRequest request = JsonConvert.DeserializeObject<PasswordChangeRequest>(rawMessage);
      UsersUtils basicDatabaseUtils = new UsersUtils(credentials);
      response.ResponseToSender = basicDatabaseUtils.ChangePassword(request);
      return response;
    }

  }
}
