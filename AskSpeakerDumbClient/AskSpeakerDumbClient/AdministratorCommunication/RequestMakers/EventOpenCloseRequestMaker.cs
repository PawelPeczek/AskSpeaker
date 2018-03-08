using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {

	public abstract class EventOpenCloseRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {
		
		protected abstract void ProvideRequestNameToRequest(RequestWithEventID request);

		protected override BaseRequest MakeRequest () {
			RequestWithEventID request = new RequestWithEventID ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest(RequestWithEventID request){
			request.EventID = ProvideValueForIDField ("EventID");
			ProvideRequestNameToRequest (request);
		}

	}
}

