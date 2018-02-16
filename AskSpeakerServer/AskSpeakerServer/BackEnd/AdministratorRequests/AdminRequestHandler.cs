using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestHandler {
		
		private IDictionary<Object, Object> Credentials;
		private Dictionary<string, object> DeserializedMsg;
		private string Message;

		public AdminRequestHandler (IDictionary<Object, Object> credentials, string message) {
			Credentials = credentials;
			Console.WriteLine ("Trying to firs deserialize to dictionary");
			DeserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, object>> (message);
			Console.WriteLine ("Deserializing to dictionary [OK]");
			Message = message;
		}

		public object ProceedRequest(){
			Console.WriteLine ("ProceedRequest() starts!");
			if (!DeserializedMsg.ContainsKey ("Request") && !DeserializedMsg.ContainsKey ("Message"))
				throw new ApplicationException ("Invalid message format.");
			AdminRequestTypes reqType;	
			if (DeserializedMsg.ContainsKey ("Request")) {
				reqType = GetRequestType ((string)DeserializedMsg ["Request"]);
			} else {
				reqType = GetRequestType ((string)DeserializedMsg ["Message"]);
			}
			Console.WriteLine ("Before dispath");
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

