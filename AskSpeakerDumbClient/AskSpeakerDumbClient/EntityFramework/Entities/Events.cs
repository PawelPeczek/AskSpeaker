using System;
using System.ComponentModel;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AskSpeakerServer.EntityFramework.Entities {
	public class Events {

		public string EventHash {
			get;
			set;
		}
			
		public string EventName {
			get;
			set;
		}

		public string EventDesc {
			get;
			set;
		}
			
		public string SpeakerName {
			get;
			set;
		}
			
		public string SpeakerSurname {
			get;
			set;
		}

		public bool Closed {
			get;
			set;
		}
	}
}

