using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {

	public abstract class EventOpenCloseRequestMaker : RequestMaker<AdminRequestTypes> {
		
		protected abstract void ProvideRequestNameToRequest(RequestWithEventHash request);

		protected override BaseRequest MakeRequest () {
			RequestWithEventHash request = new RequestWithEventHash ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest(RequestWithEventHash request){
			request.EventHash = ProceedStringValueGettingDialog ("EventHast");
			ProvideRequestNameToRequest (request);
		}

	}
}

