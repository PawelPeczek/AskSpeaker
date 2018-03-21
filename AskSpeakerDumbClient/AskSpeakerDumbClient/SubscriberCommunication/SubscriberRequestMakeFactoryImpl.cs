using System;
using AskSpeakerDumbClient.Clients;
using AskSpeakerServer.BackEnd.SubscriberRequests.RequestMakers;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestMakeFactoryImpl : RequestMakerFactory<SubscriberRequestTypes>{
		
		public RequestMaker<SubscriberRequestTypes> MakeRequest (SubscriberRequestTypes requestType) {
			RequestMaker<SubscriberRequestTypes> result;
			switch (requestType) {
				case SubscriberRequestTypes.QuestionAddRequest:
					result = new QuestionAddRequestMaker ();
					break;
				case SubscriberRequestTypes.QuestionsRequest:
					result = new QuestionsRequestMaker ();
					break;
				case SubscriberRequestTypes.VoteRequest:
					result = new VoteRequestMaker ();
					break;
				default:
					throw new NotImplementedException ("Not yet implemented.");
			}
			return result;
		}
	
	}
}

