﻿using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserCreateRequest : RegisteredRequestPrototype {

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

