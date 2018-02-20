using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class PasswordChangeSuRequest : RegisteredRequestPrototype {

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

