using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class EventCloseRequestMaker : EventOpenCloseRequestMaker {

		protected override void ProvideRequestNameToRequest (RequestWithEventID request) {
			request.Request = AdminRequestTypes.EventClose.GetRequestString ();
		}
	}
}

