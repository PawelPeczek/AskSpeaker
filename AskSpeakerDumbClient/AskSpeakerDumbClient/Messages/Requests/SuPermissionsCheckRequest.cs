using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Requests  {
	public class SuPermissionsCheckRequest {
		public string Request {
			get;
		} = AdminRequestTypes.SuPermissionsCheck.GetRequestString();
	}
}

