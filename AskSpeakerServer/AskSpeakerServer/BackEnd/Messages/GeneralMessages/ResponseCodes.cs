using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses {
	public enum ResponseCodes {
		AllOK = 0,
		DataConstraintViolated = 1,
		CannotFindRequiredDataItem = 2
	}
}

