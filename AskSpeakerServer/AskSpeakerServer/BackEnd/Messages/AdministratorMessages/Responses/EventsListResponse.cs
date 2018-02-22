using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using System.Linq;
using AskSpeakerServer.EntityFramework.Entities;

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

