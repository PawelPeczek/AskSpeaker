using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class EventCloseRequestMaker : EventOpenCloseRequestMaker {

		protected override void ProvideRequestNameToRequest (RequestWithEventHash request) {
			request.Request = AdminRequestTypes.EventClose.GetRequestString ();
		}
	}
}

