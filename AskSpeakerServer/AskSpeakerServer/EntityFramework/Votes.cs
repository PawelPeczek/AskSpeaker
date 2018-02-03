﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AskSpeakerServer {
	public class Votes {

		private short ValueStored;

		[Key]
		public int VoteID {
			get;
			set;
		}

		[Required]
		public short Value { 
			get {
				return ValueStored;
			}

			set { 
				if(value == 1 || value == -1){
					ValueStored = value;
				} else 
					throw new ArgumentException("Value must be in set {-1, 1}");
			}
		}

		[Required]
		[ForeignKey("Question")]
		public int QuestionID {
			get;
			set;
		}

		public virtual Questions Question {
			get;
			set;
		}
	}
}

