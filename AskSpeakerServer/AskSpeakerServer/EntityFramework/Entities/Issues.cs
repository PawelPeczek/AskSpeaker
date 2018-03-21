using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
	public class Issues {

		[Key]
		public int IssueID {
			get;
			set;
		}

		[Key]
		public int QuestionID {
			get;
			set;
		}

		[Required]
		[MaxLength(512)]
		public string IssueContent {
			get;
			set;
		}

		[JsonIgnore]
		public virtual Questions Question {
			get;
			set;
		}
	}
}

