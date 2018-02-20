using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public static class AdminRequestDispather {
		public static object Dispath
		(AdminRequestTypes reqType, string message, IDictionary<Object, Object> credentials) {
			AdminRequestLogic logic = new AdminRequestLogic (credentials);
			object result = null;
			try {
				switch (reqType) {
				case AdminRequestTypes.SuPermissionsCheck:
					result = logic.CheckSuPermistions ();
					break;
				case AdminRequestTypes.EventClose:
					result = logic.CloseEvent (JsonConvert.DeserializeObject<EventOpenCloseRequest>(message));
					break;
				case AdminRequestTypes.EventReOpen:
					result = logic.ReOpenEvent (JsonConvert.DeserializeObject<EventOpenCloseRequest>(message));
					break;
				case AdminRequestTypes.EventEdit:
					result = logic.EditEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(message));
					break;
				case AdminRequestTypes.EventCreate:
					result = logic.CreateEvent (JsonConvert.DeserializeObject<EventEditCreateRequest>(message));
					break;
				case AdminRequestTypes.QuestionCancell:
					result = logic.CancellQuestion(JsonConvert.DeserializeObject<QuestionCancelRequest>(message));
					break;
				case AdminRequestTypes.QuestionMerge:
					result = logic.MergeQuestions(JsonConvert.DeserializeObject<QuestionMergeRequest>(message));
					break;
				case AdminRequestTypes.QuestionEdit:
					result = logic.EditQuestion(JsonConvert.DeserializeObject<QuestionEditRequest>(message));
					break;
				case AdminRequestTypes.UserCreate:
					result = logic.CreateUser(JsonConvert.DeserializeObject<UserCreateRequest>(message));
					break;
				case AdminRequestTypes.UserDelete:
					result = logic.DeactivateUser(JsonConvert.DeserializeObject<UserDeleteRequest>(message));
					break;
				case AdminRequestTypes.PasswordChange:
					result = logic.ChangePassword(JsonConvert.DeserializeObject<PasswordChangeRequest>(message));
					break;
				case AdminRequestTypes.PasswordChangeWithSu:
					result = logic.ChangePasswordWithSuPermissions
						(JsonConvert.DeserializeObject<PasswordChangeSuRequest>(message));
					break;
				case AdminRequestTypes.EventChangeOwnership:
					result = logic.ChangeEventOwnership
						(JsonConvert.DeserializeObject<EventOwnershipChangeRequest>(message));
					break;
				}
			} catch(JsonReaderException ex){
				throw new ApplicationException (ex.Message);
			}
			return result;
		}
			
	}
}

