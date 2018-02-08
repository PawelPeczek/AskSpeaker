using System;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Linq;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.Requests;
using AskSpeakerServer.BackEnd.Messages.Responses;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestLogic {
		private IDictionary<Object, Object> Credentials;

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

		public AdminRequestLogic (IDictionary<Object, Object> credentials) {
			Credentials = credentials;
		}

		public string CheckSuPermistions() {
			Dictionary<string, string> result = new Dictionary<string, string>();
			result.Add ("Response", AdminRequestTypes.SuPermissionsCheck.GetRequestString());
			result.Add ("Permissions", AdminAuthenticationModule.IsUserSuperAdmin(Credentials).ToString());
			return JsonConvert.SerializeObject (result);
		}


		public string CloseEvent(EventCloseRequest request){
			string result = null;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				int UserID = (int)Credentials ["UserID"]; 
				Events selectedEvent = 
					(from e in ctx.Events
					 where e.EventID == request.EventID
					 select e).FirstOrDefault ();
				if (selectedEvent == null)
					throw new ApplicationException ("There is not an event with such ID.");
				if (selectedEvent.UserID != UserID && !AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
					throw new FieldAccessException("Only SuperAdmin can close event hosted by another user.");
				if (selectedEvent.Closed == false) {
					selectedEvent.Closed = true;
					ctx.SaveChanges ();
					EventCloseResponse response = new EventCloseResponse ();

					result = JsonConvert.SerializeObject (response);
				}
			}
			return result;
		}

		public string EditEvent(){
			return "";
		}
	}
}

