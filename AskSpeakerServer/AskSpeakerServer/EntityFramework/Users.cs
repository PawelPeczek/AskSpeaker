using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskSpeakerServer {
	public class Users {
		
		[Key]
		public int UserID {
			get;
			set;
		}

		[MaxLength(50)]
		public string UserName {
			get;
			set;
		}

		[MaxLength(32)]
		public byte[] Password {
			get;
			set;
		}

		[ForeignKey("UserRoles")]
		public int UserRoles_UserRoleID {
			get;
			set;
		}

		public virtual UserRoles UserRole { 
			get; 
			set;
		}

	}
}

