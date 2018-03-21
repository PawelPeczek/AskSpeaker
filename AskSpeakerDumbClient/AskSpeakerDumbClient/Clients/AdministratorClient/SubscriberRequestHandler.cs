using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerDumbClient.Clients.AdministratorClient {
	public class SubscriberRequestHandler : RequestHandler<AdminRequestTypes> {

		public SubscriberRequestHandler (AdminRequestTypes selectedType, AdminDialog dialog) {
			SelectedType = selectedType;
			Dialog = dialog;
		}

		public override BaseRequest PrepareRequest(){
			AdminRequestMakerFactoryImpl factory = new AdminRequestMakerFactoryImpl ();
			RequestMaker<AdminRequestTypes> request = factory.MakeRequest (SelectedType);
			BaseRequest result = request.PrepareRequest();
			int requestID = Dialog.AddRequestToTracker (result);
			result.RequestID = requestID;
			return result;
		}
	
	}
}

