using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.Bidirectional;

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
					result = logic.CloseEvent (JsonConvert.DeserializeObject<EventCloseMessage>(message));
					break;
				case AdminRequestTypes.EventEdit:
					result = logic.EditEvent (JsonConvert.DeserializeObject<EventEditCreateMessage>(message));
					break;
				case AdminRequestTypes.EventCreate:
					result = logic.CreateEvent (JsonConvert.DeserializeObject<EventEditCreateMessage>(message));
					break;
				case AdminRequestTypes.QuestionCancell:
					result = logic.CancellQuestion(JsonConvert.DeserializeObject<QuestionCancellMessage>(message));
					break;
				case AdminRequestTypes.QuestionMerge:
					result = logic.MergeQuestions(JsonConvert.DeserializeObject<QuestionMergeMessage>(message));
					break;
				case AdminRequestTypes.QuestionEdit:
					result = logic.EditQuestion(JsonConvert.DeserializeObject<QuestionEditMessage>(message));
					break;
				
				default:
					throw new NotImplementedException();
				}
			} catch(JsonSerializationException ex){
				throw new ApplicationException (ex.Message);
			}
			return result;
		}
			
	}
}

