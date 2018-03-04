using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations  {
	public abstract class EventEditCreateRequestMaker : RequestMaker<AdminRequestTypes> {

		protected abstract void ProvideRequestNameToRequest(EventEditCreateRequest request);

		protected override BaseRequest MakeRequest () {
			EventEditCreateRequest result = new EventEditCreateRequest ();
			FulfillEventObject (result.Event);
			ProvideRequestNameToRequest (result);
			return result;
		}

		protected void FulfillEventObject(Events eventObject){
			eventObject.EventName = ProceedStringValueGettingDialog ("EventName");
			eventObject.EventDesc = ProceedStringValueGettingDialog ("EventDesc");
			eventObject.SpeakerName = ProceedStringValueGettingDialog("SpeakerName");
			eventObject.SpeakerSurname = ProceedStringValueGettingDialog ("SpeakerSurname");
			eventObject.Closed = ProceedBoolValueGettingDialog ("Closed");
		}
	}
}

