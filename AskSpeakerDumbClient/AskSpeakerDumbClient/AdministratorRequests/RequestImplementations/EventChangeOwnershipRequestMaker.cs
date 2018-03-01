using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventChangeOwnershipRequestMaker : RequestWithIDFieldsMaker {

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

