using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public static class AdminRequestDispather : Dispather {
		private IDictionary<Object, Object> Credentials;

		public static object Dispath() {
			AdminRequestLogic logic = new AdminRequestLogic (Credentials);
			object result = null;
			try {
				switch (Message.RequestType) {
				case AdminRequestTypes.SuPermissionsCheck:
					result = logic.CheckSuPermistions ();
					break;
				case AdminRequestTypes.EventClose:
					result = logic.CloseEvent (JsonConvert.DeserializeObject<EventOpenCloseRequest>(Message));
					break;
				case AdminRequestTypes.EventReOpen:
					result = logic.ReOpenEvent (JsonConvert.DeserializeObject<EventOpenCloseRequest>(Message));
					break;
				case AdminRequestTypes.EventEdit:
					result = logic.EditEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(Message));
					break;
				case AdminRequestTypes.EventCreate:
					result = logic.CreateEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(Message));
					break;
				case AdminRequestTypes.QuestionCancell:
					result = logic.CancellQuestion(JsonConvert.DeserializeObject<QuestionCancelRequest>(Message));
					break;
				case AdminRequestTypes.QuestionMerge:
					result = logic.MergeQuestions(JsonConvert.DeserializeObject<QuestionMergeRequest>(Message));
					break;
				case AdminRequestTypes.QuestionEdit:
					result = logic.EditQuestion(JsonConvert.DeserializeObject<QuestionEditRequest>(Message));
					break;
				case AdminRequestTypes.UserCreate:
					result = logic.CreateUser(JsonConvert.DeserializeObject<UserCreateRequest>(Message));
					break;
				case AdminRequestTypes.UserDelete:
					result = logic.DeactivateUser(JsonConvert.DeserializeObject<UserDeleteRequest>(Message));
					break;
				case AdminRequestTypes.PasswordChange:
					result = logic.ChangePassword(JsonConvert.DeserializeObject<PasswordChangeRequest>(Message));
					break;
				case AdminRequestTypes.PasswordChangeWithSu:
					result = logic.ChangePasswordWithSuPermissions
						(JsonConvert.DeserializeObject<PasswordChangeSuRequest>(Message));
					break;
				case AdminRequestTypes.EventChangeOwnership:
					result = logic.ChangeEventOwnership
						(JsonConvert.DeserializeObject<EventOwnershipChangeRequest>(Message));
					break;
				}
			}  catch(JsonReaderException ex) {
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

