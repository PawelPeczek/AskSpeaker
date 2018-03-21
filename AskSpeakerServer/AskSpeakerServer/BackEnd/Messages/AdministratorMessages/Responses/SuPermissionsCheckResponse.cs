using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses {
	public class SuPermissionsCheckResponse : OperationResponse {

		public SuPermissionsCheckResponse() {
			this.Response = AdminRequestTypes.SuPermissionsCheck.GetRequestString();
		}

		public bool PermissionsGranted {
			get;
			set;
		}
	}
}

