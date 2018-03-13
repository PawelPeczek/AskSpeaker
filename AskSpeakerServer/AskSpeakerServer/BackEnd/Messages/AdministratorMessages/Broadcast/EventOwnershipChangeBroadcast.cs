using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeBroadcast : BroadcastPrototype {

		public EventOwnershipChangeBroadcast(){
			Broadcast = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public string EventHash {
			get;
			set;
		}

		[JsonIgnore]
		public int NewOwnerId {
			get;
			set;
		}

		public string NewOwnerUsername {
			get;
			set;
		}

	}
}

