using System;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Linq;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.Responses;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.Bidirectional;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestLogic {
		private IDictionary<Object, Object> Credentials;

		public static IQueryable<Events> GetEventsInfo(IDictionary<object, object> credentials){
			IQueryable<Events> result;
			using(AskSpeakerContext ctx = new AskSpeakerContext ()){
				ctx.Database.Log = s => Console.WriteLine (s);
				int userID = (int)credentials ["UserID"];
				if ((string)credentials ["Privilages"] == "SuperAdmin") {
					result = 
						from e in ctx.Events
						where e.UserID == userID
						select e;
				} else {
					result = 
						from e in ctx.Events
						select e;
				}
			}
			return result;
		}

		public AdminRequestLogic (IDictionary<Object, Object> credentials) {
			Credentials = credentials;
		}

		public SuPermissionsCheckResponse CheckSuPermistions() {
			SuPermissionsCheckResponse result = new SuPermissionsCheckResponse ();
			result.Permissions = AdminAuthenticationModule.IsUserSuperAdmin(Credentials);
			return result;
		}


		public EventCloseMessage CloseEvent(EventCloseMessage request){
			EventCloseMessage result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.EventID);
				if (selectedEvent.Closed == false) {
					selectedEvent.Closed = true;
					ctx.SaveChanges ();
				}
				result = new EventCloseMessage ();
				result.EventID = request.EventID;
			}
			return result;
		}
			
		public EventEditCreateMessage EditEvent(EventEditCreateMessage request){
			EventEditCreateMessage result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.Event.EventID);
				if(request.Event.UserID != selectedEvent.UserID && 
				   !AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
					throw new FieldAccessException("Only SuperAdmin can change the ownership of Event.");
				selectedEvent.PropertiesCopy (request.Event);
				try {
					ctx.SaveChanges();
				} catch (Exception ex){
					throw new ApplicationException ($"Broken JSON Event-serialize contract. Details: {ex.Message}");
				}
				result = new EventEditCreateMessage ();
				result.Message = AdminRequestTypes.EventEdit.GetRequestString ();
				result.Event = selectedEvent;
			}
			return result;
		}

		public EventEditCreateMessage CreateEvent(EventEditCreateMessage request){
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				if (request.Event.Questions.Any ()) {
					request.Event.Questions = new HashSet<Questions>();
				}
				request.Event.User = null;
				request.Event.User.UserID = (int)Credentials ["UserId"];
				ctx.Events.Add (request.Event);
				try {
					ctx.SaveChanges();
				} catch (Exception ex){
					throw new ApplicationException ($"Broken JSON Event-serialize contract. Details: {ex.Message}");
				}
			}
			return request;
		}

		public QuestionCancellMessage CancellQuestion(QuestionCancellMessage request){
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions question = FetchActiveQuestionWithGivenID (ctx, request.QuestionID);
				question.Anulled = true;
				ctx.SaveChanges ();
			}
			return request;
		}

		public QuestionMergeMessage MergeQuestions(QuestionMergeMessage request){
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions master = FetchActiveQuestionWithGivenID (ctx, request.MasterID);
				Questions slave = FetchActiveQuestionWithGivenID (ctx, request.SlaveID);
				if (master.EventID != slave.EventID)
					throw new ApplicationException ("Cannot merge questions associated to different events.");
				slave.Merged = master;
				ctx.SaveChanges ();
			}
			return request;
		}

		private Questions FetchActiveQuestionWithGivenID(AskSpeakerContext ctx, int QuestionID){
			Questions question = 
				(from q in ctx.Questions
					where q.Anulled == false &&
					q.QuestionID == QuestionID
					select q).FirstOrDefault();
			if (question == null)
				throw new ApplicationException ($"There is no question with such ID ({QuestionID}) or the question is closed.");
			if (!AdminAuthenticationModule.IsPermissionToEventModifGranted(question.Event.UserID, Credentials))
				throw new FieldAccessException("Only SuperAdmin can cancell a quetsion assigned to event " +
					"hosted by another user.");
		}

		private Events FetchEventWithGivenID(AskSpeakerContext ctx, int EventID){
			Events result = 
				(from e in ctx.Events
					where e.EventID == EventID
					select e).FirstOrDefault ();
			if (result == null)
				throw new ApplicationException ("There is no event with such ID.");
			if (!AdminAuthenticationModule.IsPermissionToEventModifGranted(result.UserID, Credentials))
				throw new FieldAccessException("Only SuperAdmin can close event hosted by another user.");
			return result;
		}
	}
}

