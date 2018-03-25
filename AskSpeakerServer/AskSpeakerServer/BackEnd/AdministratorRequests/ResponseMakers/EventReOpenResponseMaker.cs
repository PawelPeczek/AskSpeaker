﻿using System;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class EventReOpenResponseMaker : ResponseMaker<AdminRequestTypes> {

    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      RequestWithEventHash request = JsonConvert.DeserializeObject<RequestWithEventHash>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.ReOpenEvent(request), request.RequestID);
      return response;
    }

  }
}
