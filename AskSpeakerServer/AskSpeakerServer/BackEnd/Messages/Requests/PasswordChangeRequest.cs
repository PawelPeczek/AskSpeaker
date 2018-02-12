using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Requests {
	public class PasswordChangeRequest {

		public string Request {
			get;
		} = AdminRequestTypes.PasswordChange.GetRequestString();

		public string OldPassword {
			get;
			set;
		}

		public string NewPassword {
			get;
			set;
		}
	}
}

