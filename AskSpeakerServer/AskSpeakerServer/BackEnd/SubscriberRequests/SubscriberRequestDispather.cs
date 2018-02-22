using System;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.Extensions;
using System.Data;
using System.Collections.Generic;

namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestDispather : Dispather {

		private string Hash;

		public SubscriberRequestDispather(PreProcessedSubscriberMessage message, string hash){
			Message = message;
			Hash = hash;
		}

		public override CommunicationChunk Dispath(){
			CommunicationChunk result = new CommunicationChunk ();
			SubscriberRequestLogic logic = new SubscriberRequestLogic (Hash);
			BroadcastPrototype broadcast;
			try{
				switch (((PreProcessedSubscriberMessage)Message).RequestType) {
					case SubscriberRequestTypes.QuestionsRequest:
						result.PlainResponse = logic.ObtainQuestionsList 
						(JsonConvert.DeserializeObject<RenewQuestionsRequest>(Message.RawMessage));
						break;
					case SubscriberRequestTypes.VoteRequest:
						broadcast = 
							logic.VoteQuestion(JsonConvert.DeserializeObject<VoteQuestionRequest>(Message.RawMessage));
							PrepareSelfDomainResult(result, broadcast);
						break;
					case SubscriberRequestTypes.QuestionAddRequest:
						broadcast = 
							logic.AddQuestion(JsonConvert.DeserializeObject<QuestionAddRequest>(Message.RawMessage));
							PrepareSelfDomainResult(result, broadcast);
						break;
					default:
						throw new NotImplementedException();
				}
			} catch(JsonReaderException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.JSONContractError, ex.Message);
			} catch(KeyNotFoundException ex) {
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

