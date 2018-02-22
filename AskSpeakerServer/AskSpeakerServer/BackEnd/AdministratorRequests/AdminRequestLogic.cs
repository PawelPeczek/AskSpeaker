using System;
using System.Collections.Generic;
using AskSpeakerServer.EntityFramework;
using System.Linq;
using Newtonsoft.Json;
using AskSpeakerServer.EntityFramework.Entities;
using System.Text;
using System.Security.Cryptography;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using System.Data;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public class AdminRequestLogic {
		private IDictionary<Object, Object> Credentials;

		public static string GetEventsInfoJSON(IDictionary<object, object> credentials, int requestID = -1){
			Console.WriteLine ("GetEventsInfo fired");
			string result;
			using(AskSpeakerContext ctx = new AskSpeakerContext ()){
				int userID = (int)credentials ["UserID"];
				EventsListResponse response = new EventsListResponse ();
				if ((string)credentials ["Privilages"] == "SuperAdmin") {
					response.Events = 
						from e in ctx.Events
						where e.UserID == userID
						select e;
				} else {
					response.Events = 
						from e in ctx.Events
						select e;
				}
				Console.WriteLine ("Before error");
				response.PrepareToSend (requestID);
				result = JsonConvert.SerializeObject (response);
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

		public string ObtainEventsList(EventsListRequest request){
			return	GetEventsInfoJSON (Credentials, request.RequestID);	
		}

		public SuPermissionsCheckResponse CheckSuPermistions(SuPermissionsCheckRequest request) {
			Console.WriteLine ("CheckSuPermistions()");
			SuPermissionsCheckResponse result = new SuPermissionsCheckResponse ();
			result.PermissionsGranted = AdminAuthenticationModule.IsUserSuperAdmin(Credentials);
			result.PrepareToSend (request.RequestID);
			return result;
		}


		public EventOpenCloseBroadcast CloseEvent(EventOpenCloseRequest request){
			EventOpenCloseBroadcast result = new EventOpenCloseBroadcast();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.EventID);
				if (selectedEvent.Closed == false) {
					selectedEvent.Closed = true;
					ctx.SaveChanges ();
					result.EventID = request.RequestID;
				} else
					throw new ApplicationException ("Event already closed.");
				result.PrepareToSend (AdminRequestTypes.EventClose.GetRequestString());
			}
			return result;
		}

		public EventOpenCloseBroadcast ReOpenEvent(EventOpenCloseRequest request){
			EventOpenCloseBroadcast result = new EventOpenCloseBroadcast();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.EventID);
				if (selectedEvent.Closed == true) {
					selectedEvent.Closed = false;
					ctx.SaveChanges ();
					result.EventID = request.EventID;
				} else 
					throw new ApplicationException ("Event already opened.");
				result.PrepareToSend (AdminRequestTypes.EventReOpen.GetRequestString());
			}
			return result;
		}

		public EventEditCreateBroadcast EditEvent(EventEditCreateRequest request){
			EventEditCreateBroadcast result = new EventEditCreateBroadcast();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events selectedEvent = FetchEventWithGivenID(ctx, request.Event.EventID);
				// Hash, EventID, UserID and Closed are never copied!!!
				selectedEvent.PropertiesCopy (request.Event);
				try {
					ctx.SaveChanges();
					result.Event = selectedEvent;
				} catch (DataException ex){
					throw new DataException($"Broken JSON Event-serialize contract. Details:\n {ex.Message}");
				}
				result.PrepareToSend(AdminRequestTypes.EventEdit.GetRequestString());
			}
			return result;
		}

		public EventEditCreateBroadcast CreateEvent(EventEditCreateRequest request){
			EventEditCreateBroadcast result = new EventEditCreateBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Users eventOwner = FetchUserWithGivenID (ctx, (int)Credentials ["UserID"]);
				request.Event.User = eventOwner;
				do {
					request.Event.EventHash = Events.GenerateHash ();	
				} while(IsEventWithGivenHashExists (ctx, request.Event.EventHash));
				ctx.Events.Add (request.Event);
				try {
					ctx.SaveChanges();
					result.Event = request.Event;
				}  catch (DataException ex){
					throw new DataException($"Broken JSON Event-serialize contract. Details:\n {ex.Message}");
				}
				result.PrepareToSend(AdminRequestTypes.EventCreate.GetRequestString());
			}
			return result;
		}

		public QuestionCancelBroadcast CancellQuestion(QuestionCancelRequest request){
			QuestionCancelBroadcast result = new QuestionCancelBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions question = FetchActiveQuestionWithGivenID (ctx, request.QuestionID);
				if (!question.Anulled) {
					question.Anulled = true;
					ctx.SaveChanges ();
				} else
					throw new ApplicationException ("Question already cancelled.");
				result.PrepareToSend ();
			}
			return result;
		}

		public QuestionMergeBroadcast MergeQuestions(QuestionMergeRequest request){
			QuestionMergeBroadcast result = new QuestionMergeBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions master = FetchActiveQuestionWithGivenID (ctx, request.MasterID);
				Questions slave = FetchActiveQuestionWithGivenID (ctx, request.SlaveID);
				if (master.EventID != slave.EventID)
					throw new InvalidOperationException ("Cannot merge questions associated to different events.");
				if(master.QuestionID == slave.QuestionID)
					throw new InvalidOperationException ("Cannot merge question with itself.");
				slave.Merged = master;
				ctx.SaveChanges ();
				result.PrepareToSend ();
			}
			return result;
		}

		public QuestionEditBroadcast EditQuestion(QuestionEditRequest request) {
			QuestionEditBroadcast result = new QuestionEditBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions origin = FetchActiveQuestionWithGivenID (ctx, request.QuestionID);
				origin.QuestionContent = request.NewQuestionContent;
				try {
					ctx.SaveChanges ();
				} catch(DataException ex){
					throw new DataException ($"Broken JSON Question-serialize contract. Details: {ex.Message}");
				}
				result.PrepareToSend ();
			}
			return result;
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
				} catch (DataException ex){
					throw new DataException ($"Error while creating user. Details:\n {ex.Message}");
				}
				result.PrepareToSend (request.RequestID, AdminRequestTypes.UserCreate.GetRequestString());
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
					throw new ApplicationException("User already deleted.");
				} else {
					user.Active = false;
					ChangeEventsOwnerShip (ctx, user, request.NewEventOwnerID);
					try {
						ctx.SaveChanges();
					} catch (DataException ex){
						throw new DataException ($"Error while deleting user. Details:\n {ex.Message}");
					}
				}
				result.PrepareToSend (request.RequestID, AdminRequestTypes.UserDelete.GetRequestString ());
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
				if (user.Password.SequenceEqual(encryptedOldPasswd)) {
					byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes (request.NewPassword));
					user.Password = encryptedNewPasswd;
					ctx.SaveChanges ();
					Credentials ["PasswordChanged"] = true;
				} else {
					throw new ArgumentException ("Wrong origin password.");
				}
				result.PrepareToSend (request.RequestID, AdminRequestTypes.PasswordChange.GetRequestString());
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
				Console.WriteLine ($"New password: {request.NewPassword} for user {user.UserName}");
				SHA256 SHAEncryptor = SHA256Managed.Create ();
				byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes (request.NewPassword));
				user.Password = encryptedNewPasswd;
				ctx.SaveChanges ();
				if((int)Credentials ["UserID"] == request.UserID) Credentials ["PasswordChanged"] = true;
				result.PrepareToSend (request.RequestID, AdminRequestTypes.PasswordChangeWithSu.GetRequestString());
			}
			return result;
		}

		public EventOwnershipChangeBroadcast ChangeEventOwnership(EventOwnershipChangeRequest request){
			if (!AdminAuthenticationModule.IsUserSuperAdmin (Credentials))
				throw new UnauthorizedAccessException ("SuperUser access required.");
			EventOwnershipChangeBroadcast result = new EventOwnershipChangeBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events chosenEvent = FetchEventWithGivenID (ctx, request.EventID);
				Users user = FetchUserWithGivenID (ctx, request.newOwnerID);
				chosenEvent.User = user;
				try {
					ctx.SaveChanges ();
				} catch(DataException ex){
					throw new DataException ($"Error while changing event ownership. Details:\n {ex.Message}");
				}
				result.PrepareToSend ();
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
				throw new KeyNotFoundException ($"There is no question with such ID ({QuestionID}) " +
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
				throw new KeyNotFoundException ("There is no event with such ID.");
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
				throw new KeyNotFoundException ("No user found.");
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
				throw new KeyNotFoundException ("There is no such active user.");
			foreach (Events userEvent in user.Events) {
				userEvent.User = newOwner;
			}
		}
	}
}

