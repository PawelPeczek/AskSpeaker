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
			VotesSum = 0;
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
		public int? MergedWith {
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
		} = 0;

		public override bool Equals (object obj) {
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Questions))
				return false;
			Questions other = (Questions)obj;
			return QuestionID == other.QuestionID;
		}
		

		public override int GetHashCode () {
			unchecked {
				return QuestionID.GetHashCode ();
			}
		}
		
	}
}

