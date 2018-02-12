using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Responses {
	public class OperationResponse {

		public string Response {
			get;
			set;
		}

		public bool OperationStatus {
			get;
			set;
		}

		public string ErrorCause {
			get;
			set;
		}

	}
}

