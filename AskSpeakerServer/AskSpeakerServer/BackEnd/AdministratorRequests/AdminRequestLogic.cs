using System;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Linq;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using System.Text;
using System.Security.Cryptography;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestLogic {
		private IDictionary<Object, Object> Credentials;

		public static string GetEventsInfoJSON(IDictionary<object, object> credentials){
			Console.WriteLine ("GetEventsInfo fired");
			string result;
			using(AskSpeakerContext ctx = new AskSpeakerContext ()){
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
				Console.WriteLine ("Before error");
				result = JsonConvert.SerializeObject (events);
				Console.WriteLine ("After error");
			}
			Console.WriteLine ("GetEventsInfo returning info");
			return result;
		}

		public AdminRequestLogic (IDictionary<Object, Object> credentials) {
			Console.WriteLine ("AdminRequestLogic ctor");
			Credentials = credentials;
			if (!AdminAuthenticationModule.IsUserStillActive (credentials))
				throw new UnauthorizedAccessException ("User account was deleted.");
			if (AdminAuthenticationModule.HasPasswordForUserChanged (credentials))
				throw new PasswordHasChangedException ("Password for user has changed during this session.");
		}

		public SuPermissionsCheckResponse CheckSuPermistions() {
			Console.WriteLine ("CheckSuPermistions()");
			SuPermissionsCheckResponse result = new SuPermissionsCheckResponse ();
			result.Permissions = AdminAuthenticationModule.IsUserSuperAdmin(Credentials);
			return result;
		}


		public EventOpenCloseMessage CloseEvent(EventOpenCloseMessage request){
			EventOpenCloseMessage result = null;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.EventID);
				if (selectedEvent.Closed == false) {
					selectedEvent.Closed = true;
					ctx.SaveChanges ();
					result = request;
				}
			}
			return result;
		}

		public EventOpenCloseMessage ReOpenEvent(EventOpenCloseMessage request){
			EventOpenCloseMessage result = null;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.EventID);
				if (selectedEvent.Closed == true) {
					selectedEvent.Closed = false;
					ctx.SaveChanges ();
					result = request;
				}
			}
			return result;
		}

		public EventEditCreateMessage EditEvent(EventEditCreateMessage request){
			EventEditCreateMessage result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.Event.EventID);
				// Hash, EventID and UserID are never copied!!!
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
				Console.WriteLine ("Before trying to read credentials");
				Console.WriteLine ("Is Credential fulfilled with UserID: " + Credentials.ContainsKey("UserID"));
				Users eventOwner = FetchUserWithGivenID (ctx, (int)Credentials ["UserID"]);
				request.Event.User = eventOwner;
				Console.WriteLine ("After trying to read credentials");
				do {
					request.Event.EventHash = Events.GenerateHash ();	
				} while(IsEventWithGivenHashExists (ctx, request.Event.EventHash));
				ctx.Events.Add (request.Event);
				try {
					ctx.SaveChanges();
				} catch (Exception ex){
					throw new ApplicationException ($"Broken JSON Event-serialize contract. Details: {ex.Message}");
				}
			}
			return request;
		}

		public QuestionCancelMessage CancellQuestion(QuestionCancelMessage request){
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

		public QuestionEditMessage EditQuestion(QuestionEditMessage request) {
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions origin = FetchActiveQuestionWithGivenID (ctx, request.QuestionID);
				origin.QuestionContent = request.NewQuestionContent;
				try {
					ctx.SaveChanges ();
				} catch(Exception ex){
					throw new ApplicationException ($"Broken JSON Question-serialize contract. Details: {ex.Message}");
				}
			}
			return request;
		}
			
		public OperationResponse CreateUser(UserCreateRequest request){
			if (!AdminAuthenticationModule.IsUserSuperAdmin (Credentials))
				throw new UnauthorizedAccessException ("SuperUser access required.");
			OperationResponse result = new OperationResponse ();
			result.Response = AdminRequestTypes.UserCreate.GetRequestString ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				SHA256 SHAEncryptor = SHA256Managed.Create();
				byte[] encryptedPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes(request.Password));
				Users user = new Users ();
				user.UserName = request.UserName;
				user.Password = encryptedPasswd;
				// Not catching exception cause the lack of "Admin" UserRole 
				// indicates serious server-side error and the process should be terminated.
				UserRoles role = 
					(from ur in ctx.UserRoles
					 where ur.RoleName == "Admin"
					 select ur).First ();
				user.UserRole = role;
				ctx.Users.Add (user);
				try {
					ctx.SaveChanges();
					result.OperationStatus = true;
				} catch (Exception ex){
					result.OperationStatus = false;
					result.ErrorCause = ex.Message;
				}
			}
			return result;
		}

		public OperationResponse DeactivateUser(UserDeleteRequest request){
			if (!AdminAuthenticationModule.IsUserSuperAdmin (Credentials))
				throw new UnauthorizedAccessException ("SuperUser access required.");
			OperationResponse result = new OperationResponse ();
			result.Response = AdminRequestTypes.UserDelete.GetRequestString ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Users user = 
					(from u in ctx.Users
					 where u.UserID == request.UserID
					 select u).FirstOrDefault ();
				if (user == null)
					throw new ApplicationException ("User does not exist.");
				if (user.Active == false) {
					result.OperationStatus = false;
					result.ErrorCause = "User already deleted.";
				} else {
					user.Active = false;
					ChangeEventsOwnerShip (ctx, user, request.NewEventOwnerID);
					try {
						ctx.SaveChanges();
						result.OperationStatus = true;
					} catch (Exception ex){
						result.OperationStatus = false;
						result.ErrorCause = ex.Message;
					}
				}
			}
			return result;
		}

		public OperationResponse ChangePassword(PasswordChangeRequest request){
			OperationResponse result = new OperationResponse ();
			result.Response = AdminRequestTypes.PasswordChange.GetRequestString ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Users user = FetchUserWithGivenID (ctx, (int)Credentials ["UserID"]);
				Console.WriteLine ("User that was fetched: " + user.UserName);
				SHA256 SHAEncryptor = SHA256Managed.Create ();
				byte[] encryptedOldPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes (request.OldPassword));
				if (user != null && user.Password.SequenceEqual(encryptedOldPasswd)) {
					byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes (request.NewPassword));
					user.Password = encryptedNewPasswd;
					ctx.SaveChanges ();
					result.OperationStatus = true;
					Credentials ["PasswordChanged"] = true;
				} else {
					result.OperationStatus = false;
					result.ErrorCause = "Unresolved credentials.";
				}
			}
			return result;
		}


		public OperationResponse ChangePasswordWithSuPermissions(PasswordChangeSuRequest request){
			if (!AdminAuthenticationModule.IsUserSuperAdmin (Credentials))
				throw new UnauthorizedAccessException ("SuperUser access required.");
			OperationResponse result = new OperationResponse ();
			result.Response = AdminRequestTypes.PasswordChangeWithSu.GetRequestString ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Users user = FetchUserWithGivenID (ctx, request.UserID);
				if (user != null) {
					Console.WriteLine ($"New password: {request.NewPassword} for user {user.UserName}");
					SHA256 SHAEncryptor = SHA256Managed.Create ();
					byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes (request.NewPassword));
					user.Password = encryptedNewPasswd;
					ctx.SaveChanges ();
					result.OperationStatus = true;
					if((int)Credentials ["UserID"] == request.UserID) Credentials ["PasswordChanged"] = true;
				} else {
					result.OperationStatus = false;
					result.ErrorCause = "Unresolved credentials.";
				}
			}
			return result;
		}

		public OperationResponse ChangeEventOwnership(EventOwnershipChangeRequest request){
			if (!AdminAuthenticationModule.IsUserSuperAdmin (Credentials))
				throw new UnauthorizedAccessException ("SuperUser access required.");
			OperationResponse result = new OperationResponse ();
			result.Response = AdminRequestTypes.EventChangeOwnership.GetRequestString ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				try {
					Events chosenEvent = FetchEventWithGivenID (ctx, request.EventID);
					Users user = FetchUserWithGivenID (ctx, request.newOwnerID);
					chosenEvent.User = user;
					ctx.SaveChanges ();
					result.OperationStatus = true;
				} catch(Exception ex){
					result.OperationStatus = false;
					result.ErrorCause = ex.Message;
				}
			}
			return result;
		}

		private Questions FetchActiveQuestionWithGivenID(AskSpeakerContext ctx, int QuestionID){
			Questions question = 
				(from q in ctx.Questions
				 where q.Anulled == false &&
				     q.QuestionID == QuestionID
				 select q).FirstOrDefault ();
			if (question == null)
				throw new ApplicationException ($"There is no question with such ID ({QuestionID}) " +
												"or the question is closed.");
			if (!AdminAuthenticationModule.IsPermissionToEventModifGranted(question.Event.UserID, Credentials))
				throw new UnauthorizedAccessException("Only SuperAdmin can cancell a quetsion assigned to event " +
					"hosted by another user.");
			return question;
		}

		private Events FetchEventWithGivenID(AskSpeakerContext ctx, int EventID){
			Events result = 
				(from e in ctx.Events
					where e.EventID == EventID
					select e).FirstOrDefault ();
			if (result == null)
				throw new ApplicationException ("There is no event with such ID.");
			if (!AdminAuthenticationModule.IsPermissionToEventModifGranted(result.UserID, Credentials))
				throw new UnauthorizedAccessException("Only SuperAdmin can modify event hosted by another user.");
			return result;
		}

		private Users FetchUserWithGivenID(AskSpeakerContext ctx, int UserID){
			Users user;
			user = 
				(from u in ctx.Users
					where u.UserID == UserID &&
					u.Active == true
					select u).FirstOrDefault();
			if (user == null)
				throw new ApplicationException ("No user found.");
			return user;
		}

		private bool IsEventWithGivenHashExists(AskSpeakerContext ctx, string hash){
			return (
			     from e in ctx.Events
				 where e.EventHash == hash
				 select e
			).Any ();
		}

		private void ChangeEventsOwnerShip(AskSpeakerContext ctx, Users user, int newOwnerID){
			Users newOwner = FetchUserWithGivenID (ctx, newOwnerID);
			if (newOwner == null)
				throw new ApplicationException ("There is no such active user.");
			foreach (Events userEvent in user.Events) {
				userEvent.User = newOwner;
			}
		}
	}
}

