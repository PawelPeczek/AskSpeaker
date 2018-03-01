using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventCreateRequestMaker : EventEditCreateRequestMaker {
		protected override void ProvideRequestNameToRequestObject (EventEditCreateRequest request) {
			request.Request = AdminRequestTypes.EventCreate.GetRequestString ();
		}
	}
}

