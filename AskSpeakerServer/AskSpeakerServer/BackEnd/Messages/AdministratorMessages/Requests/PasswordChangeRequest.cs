using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class PasswordChangeRequest : RegisteredRequestPrototype {

		public PasswordChangeRequest(){
			Request = AdminRequestTypes.PasswordChange.GetRequestString();	
		}

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

