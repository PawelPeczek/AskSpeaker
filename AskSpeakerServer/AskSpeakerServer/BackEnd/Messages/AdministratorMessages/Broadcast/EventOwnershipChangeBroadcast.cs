using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeRequest : BroadcastPrototype {

		public EventOwnershipChangeRequest(){
			Broadcast = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
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

