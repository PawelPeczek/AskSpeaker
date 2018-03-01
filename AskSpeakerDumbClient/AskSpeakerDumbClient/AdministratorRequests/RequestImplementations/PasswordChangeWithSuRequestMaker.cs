using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations.Utils;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class PasswordChangeWithSuRequestMaker : RequestWithIDFieldsMaker {

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

