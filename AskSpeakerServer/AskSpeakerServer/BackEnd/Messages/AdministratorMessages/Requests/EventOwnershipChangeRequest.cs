using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventOwnershipChangeRequest : BaseRequest {

		public EventOwnershipChangeRequest(){
			Request = AdminRequestTypes.EventChangeOwnership.GetRequestString(); 
		}

		public string EventHash {
			get;
			set;
		}

		public string NewOwnerUsername {
			get;
			set;
		}
	}
}

