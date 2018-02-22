using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.Extensions;
using System.Data;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestDispather : Dispather {

		private string Hash;

		public SubscriberRequestDispather(PreProcessedMessage message, string hash){
			Message = message;
			Hash = hash;
		}

		public CommunicationChunk Dispath(){
			CommunicationChunk result = new CommunicationChunk ();
			SubscriberRequestLogic logic = new SubscriberRequestLogic (Hash);
			BroadcastPrototype broadcast;
			try{
				switch (Message.RequestType) {
					case SubscriberRequestTypes.QuestionsRequest:
						result.PlainResponse = logic.ObtainQuestionsList 
						(JsonConvert.DeserializeObject<RenewQuestionsRequest>(Message));
						break;
					case SubscriberRequestTypes.VoteRequest:
						broadcast = 
							logic.VoteQuestion(JsonConvert.DeserializeObject<VoteQuestionRequest>(Message));
						PrepareResult(result, broadcast);
						break;
					case SubscriberRequestTypes.QuestionAddRequest:
						broadcast = 
							logic.AddQuestion(JsonConvert.DeserializeObject<QuestionAddRequest>(Message));
						PrepareResult(result, broadcast);
						break;
					default:
						throw new NotImplementedException();
				}
			} catch(JsonReaderException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.JSONContractError, ex.Message);
			} catch(ApplicationException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.CannotFindRequiredDataItem, ex.Message);
			} catch(UnauthorizedAccessException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.PermissionsError, ex.Message);
			} catch(DataException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.DataConstraintViolated, ex.Message);
			}
			return result;
		}
	}
}

