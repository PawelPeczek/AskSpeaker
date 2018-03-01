using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventReOpenMaker : EventOpenCloseRequestMaker {

		protected override void ProvideRequestNameToRequest (RequestWithEventID request) {
			request.Request = AdminRequestTypes.EventReOpen.GetRequestString ();
		}
	}
}

