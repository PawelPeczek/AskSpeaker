using System;
using System.Data.Entity;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.EntityFramework {
	public class AskSpeakerContext : DbContext {
		
		public DbSet<UserRoles> UserRoles {
			get;
			set;
		}

		public DbSet<Users> Users { 
			get;
			set;
		}

		public DbSet<Events> Events {
			get;
			set;
		}

		public DbSet<Questions> Questions {
			get;
			set;
		}

		public DbSet<Votes> Votes {
			get;
			set;
		}

		public AskSpeakerContext () : base("name=AskSpeakerContext") {
		}
	}
}

