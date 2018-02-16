using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace AskSpeakerServer.EntityFramework.Entities {
	public class Events {

		public Events(){
			Closed = false;
			Questions = new HashSet<Questions>();
		}

		[JsonProperty(Required = Required.Default)]
		[Key]
		public int EventID {
			get;
			set;
		}

		[Required]
		[JsonProperty(Required = Required.AllowNull)]
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

		[JsonProperty(Required = Required.AllowNull)]
		[MaxLength(350)]
		public string EventDesc {
			get;
			set;
		}

		[JsonProperty(Required = Required.AllowNull)]
		[MaxLength(45)]
		public string SpeakerName {
			get;
			set;
		}

		[JsonProperty(Required = Required.AllowNull)]
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

		[JsonProperty(Required = Required.Default)]
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
			EventName = another.EventName;
			EventDesc = another.EventDesc;
			SpeakerName = another.SpeakerName;
			SpeakerSurname = another.SpeakerSurname;
			Closed = another.Closed;
		}

		public static string GenerateHash(){
			StringBuilder result = new StringBuilder (); 
			char[] hashChars = {
				'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
				'q', 'r', 's', 't', 'u', 'w', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8',
				'9', '0', '@', '#', '$', '%', '^', '&', '*', '(', ')', '!'
			};
			Random rnd = new Random ();
			for (int i = 0; i < 6; i++) {
				result.Append (hashChars [rnd.Next (0, hashChars.Length)]);
			}
			return result.ToString ();
		}
	}
}

