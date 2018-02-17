using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Requests {
	public class PasswordChangeSuRequest {
		
		public string Request {
			get;
		} = AdminRequestTypes.PasswordChangeWithSu.GetRequestString();

		public int UserID {
			get;
			set;
		}

		public string NewPassword {
			get;
			set;
		}
	}
}

