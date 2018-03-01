using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventChangeOwnershipRequestMaker : RequestWithEventIDUtil {

		protected override BaseRequest MakeRequest () {
			EventOwnershipChangeRequest RequestObject =  new EventOwnershipChangeRequest ();
			ProvideEventIDToRequest (RequestObject);
			ProvideNewOwnerIDToRequest (RequestObject);
			return RequestObject;
		}
			

		private void ProvideNewOwnerIDToRequest(EventOwnershipChangeRequest requestObject){
			string newOwnerID = ProceedStringValueGettingDialog("NewOwnerID");
			requestObject.newOwnerID = TryParseID(newOwnerID, "NewOwnerID");
		}

	}
}

