using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.SubscriberRequests;

namespace AskSpeakerDumbClient.Clients.SubscriberClient {
	public class SubscriberRequestHandler : RequestHandler<SubscriberRequestTypes> {

		public SubscriberRequestHandler (SubscriberRequestTypes selectedType, SubscriberDialog dialog) {
			SelectedType = selectedType;
			Dialog = dialog;
		}

		public override BaseRequest PrepareRequest(){
			SubscriberRequestMakeFactoryImpl factory = new SubscriberRequestMakeFactoryImpl ();
			RequestMaker<SubscriberRequestTypes> request = factory.MakeRequest (SelectedType);
			BaseRequest result = request.PrepareRequest();
			int requestID = Dialog.AddRequestToTracker (result);
			result.RequestID = requestID;
			return result;
		}
	
	}
}

