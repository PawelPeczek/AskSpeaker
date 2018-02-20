using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class PasswordChangeRequest : BaseRequest {

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

