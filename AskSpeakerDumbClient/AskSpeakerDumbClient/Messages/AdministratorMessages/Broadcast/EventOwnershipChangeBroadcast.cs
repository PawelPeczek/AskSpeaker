using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class EventOwnershipChangeBroadcast : BroadcastWIthEventID {

		public EventOwnershipChangeBroadcast(){
			Broadcast = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public int newOwnerID {
			get;
			set;
		}

	}
}

