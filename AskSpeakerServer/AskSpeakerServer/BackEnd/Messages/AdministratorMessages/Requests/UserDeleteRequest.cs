using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserDeleteRequest : RegisteredRequestPrototype {

		public UserDeleteRequest(){
			Request = AdminRequestTypes.UserDelete.GetRequestString();
		}

		public int UserID {
			get;
			set;
		}

		public int NewEventOwnerID {
			get;
			set;
		}
	}
}

