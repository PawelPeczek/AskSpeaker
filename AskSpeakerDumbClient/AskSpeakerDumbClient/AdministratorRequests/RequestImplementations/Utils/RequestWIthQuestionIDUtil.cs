using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils {
	public abstract class RequestWIthQuestionIDUtil : RequestMaker {

		private const string FIELD_NAME = "QuestionID";

		protected void ProvideQuestionIDToRequest(RequestWithQuestionID request){
			string eventID = ProceedStringValueGettingDialog(FIELD_NAME);
			request.QuestionID = TryParseID(eventID, FIELD_NAME);
		}
	}
}

