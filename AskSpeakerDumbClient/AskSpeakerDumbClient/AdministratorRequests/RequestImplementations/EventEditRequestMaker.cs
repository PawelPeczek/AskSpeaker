using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class EventEditRequestMaker : EventEditCreateRequestMaker {
		protected new void FulfillEventObject(Events eventObject){
			base.FulfillEventObject (eventObject);
			eventObject.EventHash = ProceedStringValueGettingDialog ("EventHash");
		}

		protected override void ProvideRequestNameToRequest (EventEditCreateRequest request) {
			request.Request = AdminRequestTypes.EventEdit.GetRequestString ();
		}

	}
}

