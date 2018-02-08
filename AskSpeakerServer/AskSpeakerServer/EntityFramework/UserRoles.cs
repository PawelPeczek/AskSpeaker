using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

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

		[Required]
		[MaxLength(50)]
		public string RoleName {
			get;
			set;
		}

		[JsonIgnore]
		public virtual ICollection<Users> Users {
			get;
			set;
		}
	}
}

