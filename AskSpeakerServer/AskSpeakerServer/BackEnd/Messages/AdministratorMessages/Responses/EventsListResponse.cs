using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.RequestHandlers;
using System.Linq;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses {
	public class EventsListResponse : OperationResponse {
		public EventsListResponse () {
			Response = AdminRequestTypes.EventsInfoRenew.GetRequestString ();
		}

		public IQueryable<Events> Events {
			get;
			set;
		}
	}
}

