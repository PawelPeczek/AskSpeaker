using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestHandler {
		
		private IDictionary<Object, Object> Container;
		private string Message;

		public AdminRequestHandler (IDictionary<Object, Object> container, string message) {
			Container = container;
			Message = message;
		}

		public string ProceedRequest(){
			Dictionary<string, string> deserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, string>> (Message);
			if (!deserializedMsg.ContainsKey ("request"))
				throw new ApplicationException ("Invalid message format.");
			AdminRequestTypes reqType = GetRequestType (deserializedMsg["request"]);

			return "";
		}

		private AdminRequestTypes GetRequestType (string requestString){
			foreach (AdminRequestTypes reqType in Enum.GetValues(typeof(AdminRequestTypes))) {
				if (requestString.ToLower () == reqType.GetRequestString ()) {
					return reqType;
				}	
			}
			throw new ApplicationException ($"Request {requestString} is not supported.");
		}

	}
}

