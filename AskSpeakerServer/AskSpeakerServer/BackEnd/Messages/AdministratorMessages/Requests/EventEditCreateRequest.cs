using System;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventEditCreateRequest : BaseRequest {

		public Events Event {
			get;
			set;
		}

	}
}

