using System;
namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers {
  public class EventEditResponseMaker : ResponseMaker<AdminRequestTypes>{
    
    public override CommunicationChunk PrepareCommunicationChunk(string rawMessage, IDictionary<object, object> credentials) {
      CommunicationChunk response = new CommunicationChunk();
      RequestWithEventHash request = JsonConvert.DeserializeObject<RequestWithEventHash>(rawMessage);
      EventsUtils eventsInfoUtils = new EventsUtils(credentials);
      PrepareOperationResponseFromBroadcast(response, eventsInfoUtils.CloseEvent(request), request.RequestID);
      return response;
    }

  }
}
