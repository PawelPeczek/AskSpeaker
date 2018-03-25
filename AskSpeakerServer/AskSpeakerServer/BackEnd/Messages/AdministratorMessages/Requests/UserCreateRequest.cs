using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class UserCreateRequest : BaseRequest {

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

