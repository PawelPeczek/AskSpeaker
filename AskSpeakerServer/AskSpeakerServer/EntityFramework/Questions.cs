using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AskSpeakerServer {
	public class Questions {
		public Questions () {
			Anulled = false;
			Votes = new HashSet<Votes>();
		}

		[Key]
		public int QuestionID {
			get;
			set;
		}

		[MaxLength(350)]
		public string QuestionContent {
			get;
			set;
		}

		public bool Anulled { 
			get; 
			set; 
		}

		[ForeignKey("Events")]
		public int EventID {
			get;
			set;
		}

		[ForeignKey("Questions")]
		public int MergedWith {
			get;
			set;
		}

		public virtual Events Event { 
			get;
			set;
		}

		public virtual Questions Merged { 
			get;
			set;
		}

		public virtual ICollection<Votes> Votes {
			get;
			set;
		}
	}
}

