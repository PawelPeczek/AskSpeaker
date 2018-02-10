using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
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
		[ForeignKey("UserRole")]
		public int UserRoleID {
			get;
			set;
		}

		[JsonIgnore]
		public virtual UserRoles UserRole { 
			get; 
			set;
		}

		[JsonIgnore]
		public virtual ICollection<Events> Events {
			get;
			set;
		}

	}
}

