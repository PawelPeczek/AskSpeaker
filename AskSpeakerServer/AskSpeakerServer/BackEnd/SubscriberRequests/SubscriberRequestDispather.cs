using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public static class SubscriberRequestDispather {
		public static CommunicationChunk Dispath(SubscriberRequestTypes reqType, 
			string message, string hash){
			CommunicationChunk result = new CommunicationChunk ();;
			SubscriberRequestLogic logic = new SubscriberRequestLogic (hash);
			BroadcastPrototype broadcast;
			try{
				switch (reqType) {
					case SubscriberRequestTypes.QuestionsRequest:
						result.PlainResponse = logic.ObtainQuestionsList 
							(JsonConvert.DeserializeObject<RenewQuestionsRequest>(message));
						break;
					case SubscriberRequestTypes.VoteRequest:
						broadcast = 
							logic.VoteQuestion(JsonConvert.DeserializeObject<VoteQuestionRequest>(message));
						PrepareResult(result, broadcast);
						break;
					case SubscriberRequestTypes.QuestionAddRequest:
						broadcast = 
							logic.AddQuestion(JsonConvert.DeserializeObject<QuestionAddRequest>(message));
						PrepareResult(result, broadcast);
						break;
					default:
					throw new NotImplementedException();
				}
			} catch(JsonReaderException ex){
				throw new ApplicationException (ex.Message);
			}
			return result;
		}

		private static void PrepareResult(CommunicationChunk result, BroadcastPrototype broadcast){
			result.SelfDomainMessage = broadcast;
			result.ResponseToSender = CommunicationChunk.PrepareResponse(broadcast); 
		}
	}
}

