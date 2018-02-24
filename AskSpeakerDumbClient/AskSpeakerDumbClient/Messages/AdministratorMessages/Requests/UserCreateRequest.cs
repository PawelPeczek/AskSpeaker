using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserCreateRequest : BaseRequest {

		public UserCreateRequest(){
			Request = AdminRequestTypes.UserCreate.GetRequestString();
		}

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

