using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeBroadcast : BroadcastWIthEventHash {

		public EventOwnershipChangeBroadcast(){
			Broadcast = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public string NewOwnerName {
			get;
			set;
		}

	}
}

