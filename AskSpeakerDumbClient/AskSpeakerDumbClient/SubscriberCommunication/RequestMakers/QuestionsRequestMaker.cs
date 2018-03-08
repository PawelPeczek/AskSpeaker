using System;
using AskSpeakerDumbClient.Clients;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.SubscriberRequests.RequestMakers {
	public class QuestionsRequestMaker : RequestMaker<SubscriberRequestTypes> {

		protected override BaseRequest MakeRequest () {
			return new QuestionsRequest ();
		}

	}
}

