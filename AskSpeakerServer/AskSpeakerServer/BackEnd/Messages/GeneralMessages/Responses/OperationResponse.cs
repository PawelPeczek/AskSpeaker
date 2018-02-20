using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public class OperationResponse : RegisteredResponsePrototype {

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

