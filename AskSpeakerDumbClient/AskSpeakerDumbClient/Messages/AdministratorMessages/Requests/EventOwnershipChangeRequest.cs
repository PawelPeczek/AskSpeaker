using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest : RequestWithEventID {

		public EventOwnershipChangeRequest(){
			Request = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public int NewOwnerID {
			get;
			set;
		}
	}
}

