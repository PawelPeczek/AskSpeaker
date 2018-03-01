using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils {
	public abstract class RequestWithEventIDUtil : RequestMaker {
		
		private const string FIELD_NAME = "EventID";

		protected void ProvideEventIDToRequest(RequestWithEventID request){
			string eventID = ProceedStringValueGettingDialog(FIELD_NAME);
			request.EventID = TryParseID(eventID, FIELD_NAME);
		}
	}
}

