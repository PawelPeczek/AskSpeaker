using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventChangeOwnershipRequestMaker : RequestMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
			EventOwnershipChangeRequest requestObject =  new EventOwnershipChangeRequest ();
			FulfillRequest (requestObject);
			return requestObject;
		}
			
		private void FulfillRequest(EventOwnershipChangeRequest requestObject){
			requestObject.EventHash = ProceedStringValueGettingDialog ("EentHash");
			requestObject.NewOwnerUsername = ProceedStringValueGettingDialog ("NewOwnerUsername");
		}

	}
}

