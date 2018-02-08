using System;

namespace AskSpeakerServer.BackEnd.Messages.Responses {
	public class SuPermissionsCheckResponse {

		public string Response {
			get;
			set;
		}

		public bool Permissions {
			get;
			set;
		}
	}
}

