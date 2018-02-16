using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
	public class Questions {
		public Questions () {
			Anulled = false;
			Votes = new HashSet<Votes>();
			VotesPlus = 0;
			VotesMinus = 0;
		}

		[Key]
		public int QuestionID {
			get;
			set;
		}

		[Required]
		[MaxLength(350)]
		public string QuestionContent {
			get;
			set;
		}

		[JsonIgnore]
		[Required]
		public bool Anulled { 
			get; 
			set; 
		}

		[JsonIgnore]
		[Required]
		[ForeignKey("Event")]
		public int EventID {
			get;
			set;
		}

		[JsonIgnore]
		[ForeignKey("Merged")]
		public int MergedWith {
			get;
			set;
		}

		[JsonIgnore]
		public virtual Events Event { 
			get;
			set;
		}

		[JsonIgnore]
		public virtual Questions Merged { 
			get;
			set;
		}

		[JsonIgnore]
		public virtual ICollection<Votes> Votes {
			get;
			set;
		}

		[NotMapped]
		public int VotesSum {
			get;
			set;
		}
	}
}

