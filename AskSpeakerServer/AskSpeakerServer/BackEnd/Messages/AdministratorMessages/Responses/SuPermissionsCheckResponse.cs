using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses {
	public class SuPermissionsCheckResponse : ResponsePrototype {

		public SuPermissionsCheckResponse(){
			this.Response = AdminRequestTypes.SuPermissionsCheck.GetRequestString();
		}

		public bool Permissions {
			get;
			set;
		}
	}
}

