using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventChangeOwnershipRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
			EventOwnershipChangeRequest requestObject =  new EventOwnershipChangeRequest ();
			FulfillRequest (requestObject);
			return requestObject;
		}
			
		private void FulfillRequest(EventOwnershipChangeRequest requestObject){
			requestObject.EventID = ProvideValueForIDField ("EventID");
			requestObject.NewOwnerID = ProvideValueForIDField ("NewOwnerID");
		}

	}
}

