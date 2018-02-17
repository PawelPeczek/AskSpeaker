using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest {

		public string Request {
			get;
		} = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 

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

