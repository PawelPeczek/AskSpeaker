using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest : BaseRequest {

		public EventOwnershipChangeRequest(){
			Request = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public int EventID {
			get;
			set;
		}

		public int newOwnerID {
			get;
			set;
		}
	}
}

