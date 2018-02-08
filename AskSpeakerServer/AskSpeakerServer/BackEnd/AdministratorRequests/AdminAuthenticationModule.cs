using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using AskSpeakerServer.EntityFramework;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace AskSpeakerServer {
	public static class AdminAuthenticationModule {
		private static SHA256 SHAEncryptor = SHA256Managed.Create();

		public static List<KeyValuePair<object, object>> ResolveCredentials(StringDictionary cookies) {
			List<KeyValuePair<object, object>> result = new List<KeyValuePair<object, object>> ();
			if (cookies.ContainsKey ("User") || cookies.ContainsKey ("PW")) {
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
			}
			return result;
		}

		public static bool IsUserSuperAdmin(IDictionary<Object, Object> credentials){
			return ((string)credentials["Privilages"] == "SuperAdmin");
		}
	}
}

