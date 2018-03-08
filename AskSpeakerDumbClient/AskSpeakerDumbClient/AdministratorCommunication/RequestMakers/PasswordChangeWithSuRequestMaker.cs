using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerDumbClient.Clients.Utils;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestMakers {
	public class PasswordChangeWithSuRequestMaker : RequestWithIDFieldsMaker<AdminRequestTypes> {

		protected override BaseRequest MakeRequest () {
			PasswordChangeSuRequest request = new PasswordChangeSuRequest ();
			FullfilRequest (request);
			return request;
		}

		private void FullfilRequest(PasswordChangeSuRequest request){
			request.UserID = ProvideValueForIDField ("UserID");
			request.NewPassword = ProceedStringValueGettingDialog ("NewPassword");
		}

	}
}

