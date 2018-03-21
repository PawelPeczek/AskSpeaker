using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserDeleteRequest : RequestWithUserID {

		public UserDeleteRequest(){
			Request = AdminRequestTypes.UserDelete.GetRequestString();
		}

		public int NewEventOwnerID {
			get;
			set;
		}

	}
}

