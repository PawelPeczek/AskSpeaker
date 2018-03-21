using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd {

	public class PreProcessedAdminMessage : PreProcessedMessage {
		
		public AdminRequestTypes RequestType {
			get;
			private set;
		}

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

