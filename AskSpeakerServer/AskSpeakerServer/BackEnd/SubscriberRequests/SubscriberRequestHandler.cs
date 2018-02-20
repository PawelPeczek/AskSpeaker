using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestHandler {

		private Dictionary<string, object> DeserializedMsg;
		private string Message;

		public SubscriberRequestHandler (string message) {
			Message = message;
			DeserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, object>> (message);
		}

		public object ProceedRequest(){
			if (!DeserializedMsg.ContainsKey ("Request"))
				throw new ApplicationException ("Invalid message format.");
			SubscriberRequestTypes reqType;
			reqType = GetRequestType ((string)DeserializedMsg ["Request"]);

			return SubscriberRequestDispather.Dispath (reqType, Message);
		}

		private SubscriberRequestTypes GetRequestType(string requestString){
			foreach (SubscriberRequestTypes reqType in Enum.GetValues(typeof(SubscriberRequestTypes))) {
				if (requestString.ToLower () == reqType.GetRequestString ()) {
					return reqType;
				}	
			}
			throw new ApplicationException ($"Request {requestString} is not supported.");
		}
	}
}

