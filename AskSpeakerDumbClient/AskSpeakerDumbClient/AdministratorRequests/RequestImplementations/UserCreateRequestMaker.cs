﻿using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class UserCreateRequestMaker : RequestMaker {

		protected override BaseRequest MakeRequest () {
			UserCreateRequest request = new UserCreateRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest (UserCreateRequest request) {
			request.UserName = ProceedStringValueGettingDialog ("UserName");
			request.Password = ProceedStringValueGettingDialog ("Password");
		}
	}
}

