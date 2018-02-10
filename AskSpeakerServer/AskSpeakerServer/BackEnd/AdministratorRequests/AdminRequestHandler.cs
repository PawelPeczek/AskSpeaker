using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestHandler {
		
		private IDictionary<Object, Object> Credentials;
		private Dictionary<string, string> DeserializedMsg;
		private string Message;

		public AdminRequestHandler (IDictionary<Object, Object> credentials, string message) {
			Credentials = credentials;
			DeserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, string>> (message);
			Message = message;
		}

		public object ProceedRequest(){
			if (!DeserializedMsg.ContainsKey ("Request"))
				throw new ApplicationException ("Invalid message format.");
			AdminRequestTypes reqType = GetRequestType (DeserializedMsg["request"]);
			return AdminRequestDispather.Dispath (reqType, Message, Credentials);
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

