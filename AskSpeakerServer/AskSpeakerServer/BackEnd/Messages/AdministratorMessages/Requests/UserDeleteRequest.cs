using System;
using AskSpeakerServer.BackEnd.RequestHandlers;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

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

