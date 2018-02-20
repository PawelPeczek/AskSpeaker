using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserDeleteRequest : BaseRequest {

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

