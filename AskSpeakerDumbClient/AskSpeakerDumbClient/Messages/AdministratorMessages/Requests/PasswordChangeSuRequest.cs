using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class PasswordChangeSuRequest : BaseRequest {

		public PasswordChangeSuRequest() {
			Request = AdminRequestTypes.PasswordChangeWithSu.GetRequestString();
		}

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

