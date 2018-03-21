using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class EventEditRequestMaker : EventEditCreateRequestMaker {
		protected override void FulfillEventObject(Events eventObject){
			eventObject.EventHash = ProceedStringValueGettingDialog ("EventHash");
			base.FulfillEventObject (eventObject);
		}

		protected override void ProvideRequestNameToRequest (EventEditCreateRequest request) {
			request.Request = AdminRequestTypes.EventEdit.GetRequestString ();
		}

	}
}

