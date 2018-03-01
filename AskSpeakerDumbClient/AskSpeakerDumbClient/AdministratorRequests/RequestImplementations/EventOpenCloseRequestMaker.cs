using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {

	public abstract class EventOpenCloseRequestMaker : RequestWithEventIDUtil {
		
		protected abstract void ProvideRequestNameToRequestObject(RequestWithEventID request);

		protected override BaseRequest MakeRequest () {
			RequestWithEventID request = new RequestWithEventID ();
			ProvideEventIDToRequest (request);
			ProvideRequestNameToRequestObject (request);
			return request;
		}

	}
}

