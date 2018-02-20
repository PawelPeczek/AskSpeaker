using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public class OperationResponse : RegisteredResponsePrototype {

		public int ErrorCode {
			get;
			set;
		} = 0;

		public string ErrorCause {
			get;
			set;
		}

	}
}

