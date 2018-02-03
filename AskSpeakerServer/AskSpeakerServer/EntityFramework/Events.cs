using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AskSpeakerServer {
	public class Events {

		public Events(){
			Closed = false;
			Questions = new HashSet<Questions>();
		}

		[Key]
		public int EventID {
			get;
			set;
		}

		[Required]
		[StringLength(6)]
		public string EventHash {
			get;
			set;
		}

		[Required]
		[MaxLength(120)]
		public string EventName {
			get;
			set;
		}

		[MaxLength(350)]
		public string EventDesc {
			get;
			set;
		}

		[MaxLength(45)]
		public string SpeakerName {
			get;
			set;
		}

		[MaxLength(45)]
		public string SpeakerSurname {
			get;
			set;
		}

		[Required]
		public bool Closed {
			get;
			set;
		}

		[Required]
		[ForeignKey("Users")]
		public int UserID {
			get;
			set;
		}

		public virtual Users User {
			get;
			set;
		}

		public virtual ICollection<Questions> Questions {
			get;
			set;
		}
	}
}

