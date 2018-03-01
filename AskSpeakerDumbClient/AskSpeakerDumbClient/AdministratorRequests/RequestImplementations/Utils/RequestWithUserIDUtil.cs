using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils {
	public abstract class RequestWithUserIDUtil : RequestMaker {

		private const string FIELD_NAME = "UserID";

		protected void ProvideUserIDToRequest(RequestWithUserID request){
			string eventID = ProceedStringValueGettingDialog(FIELD_NAME);
			request.UserID = TryParseID(eventID, FIELD_NAME);
		}
	}
}

