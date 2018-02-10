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
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				byte[] encryptedPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes(cookies ["PW"]));
				string userName = cookies ["User"];
				Users user = 
					(from u in ctx.Users
						where u.UserName == userName &&
						u.Password == encryptedPasswd
						select u).First();
				if (user == null) {
					throw new ApplicationException ();
				}
				result.Add (new KeyValuePair<object, object>("UserID", user.UserID));
				result.Add (new KeyValuePair<object, object>("Privilages", user.UserRole.RoleName));
			}
			return result;
		}

		public static bool IsUserSuperAdmin(IDictionary<Object, Object> credentials){
			return ((string)credentials["Privilages"] == "SuperAdmin");
		}

		public static bool IsPermissionToEventModifGranted(int EventCreatorID, IDictionary<Object, Object> credentials){
			return (EventCreatorID == ((int)credentials ["UserID"]) || 
					AdminAuthenticationModule.IsUserSuperAdmin (credentials));
		}
			
	}
}

