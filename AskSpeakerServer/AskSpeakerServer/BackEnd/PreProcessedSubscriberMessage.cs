using System;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests;

namespace AskSpeakerServer.BackEnd {
  public class PreProcessedSubscriberMessage : PreProcessedMessage <SubscriberRequestTypes> {

    public PreProcessedSubscriberMessage (string message) : base (message) {}

		protected override void SetRequestType (string requestString) {
			RequestType = GetRequestType (requestString);
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

