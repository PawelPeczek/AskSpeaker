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
			if (cookies.ContainsKey ("user") || cookies.ContainsKey ("pw")) {
				using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
					byte[] encryptedPasswd = SHAEncryptor.ComputeHash (Encoding.Unicode.GetBytes(cookies ["pw"]));
					string userName = cookies ["user"];
					Users user = 
						(from u in ctx.Users
							where u.UserName == userName &&
							u.Password == encryptedPasswd
							select u).First();
					if (user == null) {
						throw new ApplicationException ();
					}
					result.Add (new KeyValuePair<object, object>("UserID", user.UserID));
					result.Add (new KeyValuePair<object, object>("Privilage", user.UserRole.RoleName));
				}
			}
			return result;
		}
	}
}

