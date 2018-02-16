using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Requests {
	public class UserDeleteRequest {

		public string Request {
			get;
		} = AdminRequestTypes.UserDelete.GetRequestString();

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

