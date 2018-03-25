using System;
using System.Collections.Generic;
using System.Data;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils {
  public class EventsUtils : BasicDatabaseUtils {
    
    public EventsUtils(IDictionary<Object, Object> credentials) : base(credentials) { }

    public string GetEventsInfoJSON(EventsListRequest request) {
      string result;
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        int userID = (int)Credentials["UserID"];
        EventsListResponse response = new EventsListResponse();

        if((string)Credentials["Privilages"] != "SuperAdmin") {
          response.Events = (from e in ctx.Events
                             where e.UserID == userID
                             select e);
        } else {
          response.Events = (from e in ctx.Events
                             select e);
        }
        Console.WriteLine("Before error");
        response.PrepareToSend(request.RequestID);
        result = JsonConvert.SerializeObject(response);
        Console.WriteLine("After error");
      }
      Console.WriteLine("GetEventsInfo returning info");
      return result;
    }

    public BroadcastWIthEventHash CloseEvent(RequestWithEventHash request) {
      BroadcastWIthEventHash result = new BroadcastWIthEventHash();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Events selectedEvent = FetchEventWithGivenHash(ctx, request.EventHash);
        if(selectedEvent.Closed == false) {
          selectedEvent.Closed = true;
          ctx.SaveChanges();
          result.EventHash = selectedEvent.EventHash;
        } else
          throw new ApplicationException("Event already closed.");
        result.PrepareToSend(AdminRequestTypes.EventClose.GetRequestString());
      }
      return result;
    }

    public BroadcastWIthEventHash ReOpenEvent(RequestWithEventHash request) {
      BroadcastWIthEventHash result = new BroadcastWIthEventHash();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Events selectedEvent = FetchEventWithGivenHash(ctx, request.EventHash);
        if(selectedEvent.Closed == true) {
          selectedEvent.Closed = false;
          ctx.SaveChanges();
          result.EventHash = selectedEvent.EventHash;
        } else
          throw new ApplicationException("Event already opened.");
        result.PrepareToSend(AdminRequestTypes.EventReOpen.GetRequestString());
      }
      return result;
    }

    public EventEditCreateBroadcast EditEvent(EventEditCreateRequest request) {
      EventEditCreateBroadcast result = new EventEditCreateBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Events selectedEvent = FetchEventWithGivenHash(ctx, request.Event.EventHash);
        // Hash, EventID, UserID and Closed are never copied!!!
        selectedEvent.PropertiesCopy(request.Event);
        try {
          ctx.SaveChanges();
          result.Event = selectedEvent;
        } catch(DataException ex) {
          throw new DataException($"Broken JSON Event-serialize contract. Details:\n {ex.Message}");
        }
        result.PrepareToSend(AdminRequestTypes.EventEdit.GetRequestString());
      }
      return result;
    }

    public EventEditCreateBroadcast CreateEvent(EventEditCreateRequest request) {
      EventEditCreateBroadcast result = new EventEditCreateBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Users eventOwner = FetchUserWithGivenID(ctx, (int)Credentials["UserID"]);
        request.Event.User = eventOwner;
        do {
          request.Event.EventHash = Events.GenerateHash();
        } while(IsEventWithGivenHashExists(ctx, request.Event.EventHash));
        ctx.Events.Add(request.Event);
        try {
          ctx.SaveChanges();
          result.Event = request.Event;
        } catch(DataException ex) {
          throw new DataException($"Broken JSON Event-serialize contract. Details:\n {ex.Message}");
        }
        result.PrepareToSend(AdminRequestTypes.EventCreate.GetRequestString());
      }
      return result;
    }

    public EventOwnershipChangeBroadcast ChangeEventOwnership(EventOwnershipChangeRequest request) {
      if(!AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
        throw new UnauthorizedAccessException("SuperUser access required.");
      EventOwnershipChangeBroadcast result = new EventOwnershipChangeBroadcast();
      Console.WriteLine("ChangeEventOwnership fired");
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Console.WriteLine("Hash to seek: " + request.EventHash);
        Events chosenEvent = FetchEventWithGivenHash(ctx, request.EventHash);
        Console.WriteLine("Event fetched!");
        Users user = FetchUserWithGivenUsername(ctx, request.NewOwnerUsername);
        Console.WriteLine("User fetched!");
        chosenEvent.User = user;
        try {
          ctx.SaveChanges();
          result.EventHash = request.EventHash;
          result.NewOwnerName = request.NewOwnerUsername;
          result.NewOwnerId = user.UserID;
          Console.WriteLine("Data saved");
        } catch(DataException ex) {
          throw new DataException($"Error while changing event ownership. Details:\n {ex.Message}");
        }
        result.PrepareToSend();
      }
      Console.WriteLine("ChangeEventOwnership done");
      return result;
    }

  }
}
