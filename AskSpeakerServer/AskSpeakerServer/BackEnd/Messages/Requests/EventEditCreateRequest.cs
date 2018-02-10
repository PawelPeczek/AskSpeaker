using System;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.Messages.Requests {
	public class EventEditCreateRequest {

		public string Request {
			get;
			set;
		}

		public Events Event {
			get;
			set;
		}

	}
}

