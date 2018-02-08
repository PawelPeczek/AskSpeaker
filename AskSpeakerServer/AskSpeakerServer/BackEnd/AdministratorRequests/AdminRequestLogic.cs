using System;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Linq;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestLogic {
		private IDictionary<Object, Object> SessionContainer;
		private string Message;

		public static string GetJsonEventsInfo(IDictionary<object, object> credentials){
			string result;
			using(AskSpeakerContext ctx = new AskSpeakerContext ()){
				ctx.Database.Log = s => Console.WriteLine (s);
				int userID = (int)credentials ["UserID"];
				IQueryable<Events> events;
				if ((string)credentials ["Privilages"] == "SuperAdmin") {
					events = 
						from e in ctx.Events
						where e.UserID == userID
						select e;
				} else {
					events = 
						from e in ctx.Events
						select e;
				}
				result = JsonConvert.SerializeObject(events);
			}
			return result;
		}

		public AdminRequestLogic (string message, IDictionary<Object, Object> credentials) {
			SessionContainer = credentials;
			Message = message;
		}



	}
}

