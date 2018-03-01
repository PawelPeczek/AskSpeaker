using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations {
	public class PasswordChangeRequestMaker : RequestMaker {
		
		protected override BaseRequest MakeRequest () {
			PasswordChangeRequest request = new PasswordChangeRequest ();
			FulfillRequest (request);
			return request;
		}

		private void FulfillRequest(PasswordChangeRequest request){
			request.OldPassword = ProceedStringValueGettingDialog ("OldPassword");
			request.NewPassword = ProceedStringValueGettingDialog ("NewPassword");
		}

	}
}

