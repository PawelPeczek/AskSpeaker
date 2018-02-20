using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public static class SubscriberRequestDispather {
		public static object Dispath(SubscriberRequestTypes reqType, string message, string hash){
			object result;
			SubscriberRequestLogic logic = new SubscriberRequestLogic (hash);
			try{
				switch (reqType) {
					case SubscriberRequestTypes.QuestionsRequest:
						result = logic.ObtainQuestionsList 
							(JsonConvert.DeserializeObject<RenewQuestionsRequest>(message));
						break;
					case SubscriberRequestTypes.VoteRequest:
						result = logic.VoteQuestion(JsonConvert.DeserializeObject<VoteQuestionRequest>(message));
						break;
					case SubscriberRequestTypes.QuestionAddRequest:
						result = logic.AddQuestion(JsonConvert.DeserializeObject<QuestionAddRequest>(message));
						break;
					default:
					throw new NotImplementedException();
				}
			} catch(JsonReaderException ex){
				throw new ApplicationException (ex.Message);
			}
			return result;
		}
	}
}

