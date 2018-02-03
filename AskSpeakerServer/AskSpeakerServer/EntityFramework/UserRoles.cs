using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AskSpeakerServer {
	public class UserRoles {

		public UserRoles(){
			Users = new HashSet<Users>();
		}

		[Key]
		public int UserRoleID {
			get;
			set;
		}

		[MaxLength(50)]
		public string RoleName {
			get;
			set;
		}

		public virtual ICollection<Users> Users {
			get;
			set;
		}
	}
}

