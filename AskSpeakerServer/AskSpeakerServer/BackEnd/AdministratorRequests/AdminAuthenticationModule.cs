using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using AskSpeakerServer.EntityFramework;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public static class AdminAuthenticationModule {
		private static SHA256 SHAEncryptor = SHA256Managed.Create();

		public static List<KeyValuePair<object, object>> ResolveCredentials(StringDictionary cookies) {
			if (!cookies.ContainsKey ("User") || !cookies.ContainsKey ("PW"))
				throw new ApplicationException ("Invalid cookies header in request!");
			List<KeyValuePair<object, object>> result = new List<KeyValuePair<object, object>> ();
			byte[] encryptedPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes(cookies["PW"]));
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				string userName = cookies ["User"];
				Users user = 
					(from u in ctx.Users
						where u.UserName == userName &&
						u.Password == encryptedPasswd &&
						u.Active == true
						select u).FirstOrDefault();
				if (user == null)
					throw new UnauthorizedAccessException ("Credentials don't match.");
				
				result.Add (new KeyValuePair<object, object>("UserID", user.UserID));
				result.Add (new KeyValuePair<object, object>("Privilages", user.UserRole.RoleName));
				result.Add (new KeyValuePair<object, object>("PasswordChanged", false));
			}
			return result;
		}

		public static bool IsUserSuperAdmin(IDictionary<Object, Object> credentials){
			return ((string)credentials["Privilages"] == "SuperAdmin");
		}

		public static bool IsPermissionToEventModifGranted(int EventCreatorID, IDictionary<Object, Object> credentials){
			int UserID = (int)credentials ["UserID"];
			return (EventCreatorID == UserID || 
					AdminAuthenticationModule.IsUserSuperAdmin (credentials));
		}
			
		public static bool IsUserStillActive(IDictionary<Object, Object> credentials){
			Console.WriteLine ("IsUserStillActive");
			bool result = true;
			int UserID = (int)credentials ["UserID"];
			Console.WriteLine ("After reading credentials");
			using(AskSpeakerContext ctx = new AskSpeakerContext()){
				Users user = 
					(from u in ctx.Users
						where u.UserID == UserID &&
						u.Active == true
						select u).FirstOrDefault ();
				if (user == null)
					result = false;
			}
			return result;
		}

		public static bool HasPasswordForUserChanged(IDictionary<Object, Object> credentials){
			return (bool)credentials ["PasswordChanged"];
		}
	}
}

