using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
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
		[ForeignKey("User")]
		public int UserID {
			get;
			set;
		}

		[JsonIgnore]
		public virtual Users User {
			get;
			set;
		}

		[JsonIgnore]
		public virtual ICollection<Questions> Questions {
			get;
			set;
		}

		public void PropertiesCopy(Events another){
			EventID = another.EventID;
			EventHash = another.EventHash;
			EventName = another.EventName;
			EventDesc = another.EventDesc;
			SpeakerName = another.SpeakerName;
			SpeakerSurname = another.SpeakerSurname;
			Closed = another.Closed;
			UserID = another.UserID;
		}
	}
}

