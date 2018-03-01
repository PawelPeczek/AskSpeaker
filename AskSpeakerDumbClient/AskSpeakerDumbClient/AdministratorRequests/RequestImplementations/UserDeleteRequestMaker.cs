using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations  {
	public class UserDeleteRequestMaker : RequestWithIDFieldsMaker {

		protected override AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests.BaseRequest MakeRequest () {
			UserDeleteRequest request = new UserDeleteRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest (UserDeleteRequest request) {
			request.UserID = ProvideValueForIDField ("UserID");
			request.NewEventOwnerID = ProvideValueForIDField ("NewEventOwnerID");
		}
	}
}

