using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

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

