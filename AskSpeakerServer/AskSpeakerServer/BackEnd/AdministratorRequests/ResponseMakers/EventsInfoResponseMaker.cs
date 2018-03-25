using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class EventsInfoResponseMaker : ResponseMaker<AdminRequestTypes> {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<Object, Object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      EventsListRequest request = JsonConvert.DeserializeObject<EventsListRequest>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      response.PlainResponse = eventsInfoUtils.GetEventsInfoJSON(request);
      return response;
    }

  }
}
