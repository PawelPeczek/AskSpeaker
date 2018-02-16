using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Requests {
	public class UserCreateRequest {

		public string Request {
			get;
		} = AdminRequestTypes.UserCreate.GetRequestString();

		public string UserName {
			get;
			set;
		}

		public string Password {
			get;
			set;
		}

	}
}

