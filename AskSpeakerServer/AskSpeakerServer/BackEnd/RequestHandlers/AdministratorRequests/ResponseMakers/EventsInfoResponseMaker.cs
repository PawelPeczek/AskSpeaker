using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers {
  public class EventsInfoResponseMaker : AdminResponseMaker {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<Object, Object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      EventsListRequest request = JsonConvert.DeserializeObject<EventsListRequest>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      response.PlainResponse = eventsInfoUtils.GetEventsInfoJSON(request);
      return response;
    }

  }
}
