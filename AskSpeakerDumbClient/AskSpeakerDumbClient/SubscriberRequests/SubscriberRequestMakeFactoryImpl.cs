using System;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestMakeFactoryImpl : RequestMakerFactory<SubscriberRequestTypes>{
		
		public RequestMaker<SubscriberRequestTypes> MakeRequest (SubscriberRequestTypes requestType) {
			throw new NotImplementedException ();
		}
	
	}
}

