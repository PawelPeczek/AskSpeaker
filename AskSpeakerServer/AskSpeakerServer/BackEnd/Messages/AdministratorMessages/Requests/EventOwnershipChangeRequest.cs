using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest : RegisteredRequestPrototype {

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

