using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public static class AdminRequestDispather {
		public static string Dispath
		(AdminRequestTypes reqType, string message, IDictionary<Object, Object> credentials) {
			AdminRequestLogic logic = new AdminRequestLogic (credentials);
			string result = null;
			try {
				switch (reqType) {
				case AdminRequestTypes.SuPermissionsCheck:
					result = logic.CheckSuPermistions ();
					break;
				case AdminRequestTypes.EventClose:
					EventCloseRequest request = JsonConvert.DeserializeObject<EventCloseRequest>(message);
					result = logic.CloseEvent (request);
					break;
				case AdminRequestTypes.EventEdit:
					result = logic.EditEvent ();
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

