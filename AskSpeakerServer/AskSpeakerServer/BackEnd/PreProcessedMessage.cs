using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd {
	public abstract class PreProcessedMessage<T> {

		public string RawMessage {
			get;
			protected set;
		}

    public T RequestType {
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
			Console.WriteLine ("Before SetRequestType");
			SetRequestType ((string)DeserializedMsg ["Request"]);
			Console.WriteLine ("After SetRequestType");
			Console.WriteLine (DeserializedMsg ["RequestID"].GetType());
			try {
				RequestID = Convert.ToInt32((Int64)DeserializedMsg ["RequestID"]);
			} catch(OverflowException ex){
				throw new ApplicationException (ex.Message);
			}

			Console.WriteLine ("After casting RequestID");
		}


		protected abstract void SetRequestType (string requestString);
	}
}

