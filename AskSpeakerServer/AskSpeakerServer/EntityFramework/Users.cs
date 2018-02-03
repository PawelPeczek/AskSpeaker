using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AskSpeakerServer {
	public class Users {

		public Users(){
			Events = new HashSet<Events>();
		}

		[Key]
		public int UserID {
			get;
			set;
		}

		[Required]
		[MaxLength(50)]
		public string UserName {
			get;
			set;
		}

		[Required]
		[MaxLength(32)]
		public byte[] Password {
			get;
			set;
		}

		[Required]
		[ForeignKey("UserRoles")]
		public int UserRoleID {
			get;
			set;
		}

		public virtual UserRoles UserRole { 
			get; 
			set;
		}

		public virtual ICollection<Events> Events {
			get;
			set;
		}

	}
}

