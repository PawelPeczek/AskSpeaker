using System;
using AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests{
	public class QuestionsRequest : BaseRequest {

		public QuestionsRequest () {
			Request = SubscriberRequestTypes.QuestionsRequest.GetRequestString();
		}

	}
}

