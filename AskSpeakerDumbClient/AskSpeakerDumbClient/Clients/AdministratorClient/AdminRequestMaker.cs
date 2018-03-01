using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerDumbClient.Clients.AdministratorClient  {
	public class AdminRequestMaker {

		private AdminRequestTypes SelectedType;
		private AdminDialog Dialog;

		public AdminRequestMaker (AdminRequestTypes selectedType, AdminDialog dialog) {
			SelectedType = selectedType;
			Dialog = dialog;
		}

		public BaseRequest PrepareRequest(){
			RequestMakerFactoryImpl factory = new RequestMakerFactoryImpl ();
			RequestMaker request = factory.MakeRequest (SelectedType);
			BaseRequest result = request.PrepareRequest();
			Dialog.AddRequestToTracker (result);
			return result;
		}
	
	}
}

