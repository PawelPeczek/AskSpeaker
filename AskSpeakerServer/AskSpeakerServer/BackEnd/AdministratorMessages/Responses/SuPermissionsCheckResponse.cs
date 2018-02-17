using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Responses {
	public class SuPermissionsCheckResponse {

		public string Response {
			get;
		} = AdminRequestTypes.SuPermissionsCheck.GetRequestString();

		public bool Permissions {
			get;
			set;
		}
	}
}

