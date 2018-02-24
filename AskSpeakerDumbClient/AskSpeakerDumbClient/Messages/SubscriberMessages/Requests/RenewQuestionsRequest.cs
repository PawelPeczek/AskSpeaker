using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests{
	public class RenewQuestionsRequest : BaseRequest {

		public RenewQuestionsRequest () {
			Request = SubscriberRequestTypes.QuestionsRequest.GetRequestString();
		}

	}
}

