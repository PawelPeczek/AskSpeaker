using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeBroadcast : BroadcastPrototype {

		public EventOwnershipChangeBroadcast(){
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

