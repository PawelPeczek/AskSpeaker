using System;
using System.Data.Entity;

namespace AskSpeakerServer.EntityFramework {
	public class AskSpeakerContext : DbContext {
		
		public DbSet<UserRoles> UserRoles {
			get;
			set;
		}

		public AskSpeakerContext () : base("name=AskSpeakerContext") {
			
		}
	}
}

