using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests  {
	public class SuPermissionsCheckRequest : BaseRequest {
		public SuPermissionsCheckRequest () {
			Request = AdminRequestTypes.SuPermissionsCheck.GetRequestString();
		}
	}
}

