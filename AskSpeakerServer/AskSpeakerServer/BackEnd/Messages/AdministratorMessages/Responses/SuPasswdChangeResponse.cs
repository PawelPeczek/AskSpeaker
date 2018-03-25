using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses {
	public class SuPasswdChangeResponse : OperationResponse {
		public SuPasswdChangeResponse () {
			Response = AdminRequestTypes.PasswordChangeWithSu.GetRequestString ();
		}

		[JsonIgnore]
		public int UserID {
			get;
			set;
		}
	}
}

