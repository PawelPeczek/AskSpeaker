using System;
using System.ComponentModel.DataAnnotations;

namespace AskSpeakerServer {
	public class UserRoles {
		public int UserRoleID {
			get;
			set;
		}
		[StringLength(50)]
		public string RoleName {
			get;
			set;
		}
	}
}

