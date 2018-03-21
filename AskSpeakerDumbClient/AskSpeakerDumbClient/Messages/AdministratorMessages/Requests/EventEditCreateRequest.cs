using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventEditCreateRequest : BaseRequest {

		public EventEditCreateRequest(){
			Event = new Events ();
		}

		public Events Event {
			get;
			set;
		}

	}
}

