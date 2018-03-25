using System;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd {

	public class PreProcessedAdminMessage : PreProcessedMessage<AdminRequestTypes> {

		public PreProcessedAdminMessage (string message) : base (message) {}
		
		protected override void SetRequestType (string requestString) {
			RequestType = GetRequestType (requestString);
		}

		private AdminRequestTypes GetRequestType(string requestString){
			foreach (AdminRequestTypes reqType in Enum.GetValues(typeof(AdminRequestTypes))) {
				if (requestString.ToLower () == reqType.GetRequestString ()) {
					return reqType;
				}	
			}
			throw new ApplicationException ($"Request {requestString} is not supported.");
		}
	}
}

