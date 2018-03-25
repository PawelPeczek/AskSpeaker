using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers {
  public class EventChangeOwnershipResponseMaker : AdminResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      EventOwnershipChangeRequest request = JsonConvert.DeserializeObject<EventOwnershipChangeRequest>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.ChangeEventOwnership(request), request.RequestID);
      return response;
    }

  }
}
