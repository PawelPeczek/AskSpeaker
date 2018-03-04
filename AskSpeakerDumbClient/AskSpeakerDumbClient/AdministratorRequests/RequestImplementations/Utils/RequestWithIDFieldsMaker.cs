using System;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils {
	public abstract class RequestWithIDFieldsMaker : RequestMaker<AdminRequestTypes> {
		
		protected int ProvideValueForIDField(string fieldName){
			string eventID = ProceedStringValueGettingDialog(fieldName);
			return TryParseID(eventID, fieldName);
		}

		private int TryParseID(string value, string IDName){
			int result;
			if (!int.TryParse (value, out result) || result < 0)
				throw new ArgumentException ($"Value is not a valid {IDName}");
			return result;
		}
	}
}

