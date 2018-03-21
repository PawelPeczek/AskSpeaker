using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages;
using System.Data;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestDispather : Dispather {
		private IDictionary<Object, Object> Credentials;

		public AdminRequestDispather (PreProcessedAdminMessage message, IDictionary<Object, Object> credentials){
			Message = message;
			Credentials = credentials;
		}

		public override CommunicationChunk Dispath() {
			AdminRequestLogic logic = new AdminRequestLogic (Credentials);
			CommunicationChunk result = new CommunicationChunk ();
			BroadcastPrototype broadcast;
			try {
				switch (((PreProcessedAdminMessage)Message).RequestType) {
				case AdminRequestTypes.EventsInfoRenew:
					result.PlainResponse = logic.ObtainEventsList(JsonConvert.DeserializeObject<EventsListRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.SuPermissionsCheck:
					result.ResponseToSender = logic.CheckSuPermistions (JsonConvert.DeserializeObject<SuPermissionsCheckRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.EventClose:
                        broadcast = logic.CloseEvent (JsonConvert.DeserializeObject<RequestWithEventHash>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.EventReOpen:
                        broadcast = logic.ReOpenEvent (JsonConvert.DeserializeObject<RequestWithEventHash>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.EventEdit:
					broadcast = logic.EditEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.EventCreate:
					broadcast = logic.CreateEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.QuestionCancell:
					broadcast = logic.CancellQuestion(JsonConvert.DeserializeObject<QuestionCancelRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.QuestionMerge:
					broadcast = logic.MergeQuestions(JsonConvert.DeserializeObject<QuestionMergeRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.QuestionEdit:
					broadcast = logic.EditQuestion(JsonConvert.DeserializeObject<QuestionEditRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				case AdminRequestTypes.UserCreate:
					result.ResponseToSender = logic.CreateUser(JsonConvert.DeserializeObject<UserCreateRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.UserDelete:
					result.ResponseToSender = logic.DeactivateUser(JsonConvert.DeserializeObject<UserDeleteRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.PasswordChange:
					result.ResponseToSender = logic.ChangePassword(JsonConvert.DeserializeObject<PasswordChangeRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.PasswordChangeWithSu:
					result.ResponseToSender = logic.ChangePasswordWithSuPermissions
						(JsonConvert.DeserializeObject<PasswordChangeSuRequest>(Message.RawMessage));
					break;
				case AdminRequestTypes.EventChangeOwnership:
					broadcast = logic.ChangeEventOwnership
						(JsonConvert.DeserializeObject<EventOwnershipChangeRequest>(Message.RawMessage));
					PrepareOperationResponseFromBroadcast(result, broadcast);
					break;
				}
			} catch(JsonReaderException) {
				throw new ApplicationException ("Invalid JSON request message.");
				//result.ResponseToSender = PrepareErrorResponse (ResponseCodes.JSONContractError, ex.Message);
			} catch(ApplicationException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.ActivityAlreadyDone, ex.Message);
			} catch(UnauthorizedAccessException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.PermissionsError, ex.Message);
			} catch (InvalidOperationException ex){
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.InvalidOperation, ex.Message);
			} catch(ArgumentException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.WrongOriginData, ex.Message);
			} catch (KeyNotFoundException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.CannotFindRequiredDataItem, ex.Message);
			} catch(DataException ex) {
				result.ResponseToSender = PrepareErrorResponse (ResponseCodes.DataConstraintViolated, ex.Message);
			}
			return result;
		}
			
	}
}

