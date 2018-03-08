using System;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers  {
	public class UserDeleteRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
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

