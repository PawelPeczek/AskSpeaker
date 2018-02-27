using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
	public class Questions {
		public Questions () {
			VotesSum = 0;
		}
			
		public int QuestionID {
			get;
			set;
		}
			
		public string QuestionContent {
			get;
			set;
		}


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

