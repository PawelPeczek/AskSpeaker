using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd {
	public abstract class PreProcessedMessage {

		public string RawMessage {
			get;
			protected set;
		}

		public string Request {
			get;
			protected set;
		}

		public int RequestID {
			get;
			protected set;
		}

		public PreProcessedMessage(string message){
			Dictionary<string, object> DeserializedMsg = 
				JsonConvert.DeserializeObject<Dictionary<string, object>> (message);
			if (!DeserializedMsg.ContainsKey ("Request") && !DeserializedMsg.ContainsKey ("RequestID"))
				throw new ApplicationException ("Invalid message format.");
			RawMessage = message;
			SetRequestType ((string)DeserializedMsg ["Request"]);
			RequestID = (int)DeserializedMsg ["RequestID"];
		}

		protected abstract void SetRequestType (string requestString);
	}
}

