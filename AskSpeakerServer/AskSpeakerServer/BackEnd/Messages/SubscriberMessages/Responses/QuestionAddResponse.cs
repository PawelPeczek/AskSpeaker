using System;
using AskSpeakerServer.BackEnd.SubscriberRequests;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.Messages.Prototypes;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Responses {
	public class QuestionAddResponse : RegisteredResponsePrototype {

		public QuestionAddResponse(){
			Response = SubscriberRequestTypes.QuestionAddRequest.GetRequestString();
		}

		public int ErrorCode {
			get;
			set;
		} = 0;
			
		[JsonProperty(Required = Required.AllowNull)]
		public Questions Question {
			get;
			set;
		}

		public string ErrorMessage {
			get;
			set;
		}
	}
}

