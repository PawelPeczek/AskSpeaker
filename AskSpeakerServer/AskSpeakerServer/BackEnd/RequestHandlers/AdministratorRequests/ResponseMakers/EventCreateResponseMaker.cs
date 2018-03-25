using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers {
  public class EventCreateResponseMaker : AdminResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      EventEditCreateRequest request = JsonConvert.DeserializeObject<EventEditCreateRequest>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.CreateEvent(request), request.RequestID);
      return response;
    }

  }
}
