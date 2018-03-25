using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class PasswordChangeSuRequest : RequestWithUserID {

		public PasswordChangeSuRequest() {
			Request = AdminRequestTypes.PasswordChangeWithSu.GetRequestString();
		}

		public string NewPassword {
			get;
			set;
		}
	}
}

