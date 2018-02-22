using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd {

	public class PreProcessedMessage {
		
		public string Request {
			get;
			set;
		}

		public SubscriberRequestTypes RequestType {
			get;
			set;
		}

		public int RequestID {
			get;
			set;
		}
			
		public PreProcessedMessage(string message){
			Dictionary<string, object> DeserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, object>> (message);
			if (!DeserializedMsg.ContainsKey ("Request") && !DeserializedMsg.ContainsKey ("RequestID"))
				throw new ApplicationException ("Invalid message format.");
			Message = message;
			RequestType = GetRequestType ((string)DeserializedMsg ["Request"]);
			RequestID = (int)DeserializedMsg ["RequestID"];
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

