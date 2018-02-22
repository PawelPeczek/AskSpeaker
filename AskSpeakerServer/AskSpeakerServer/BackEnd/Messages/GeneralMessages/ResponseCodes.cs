using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages {
	public enum ResponseCodes {
		AllOK = 0,
		DataConstraintViolated = 1,
		CannotFindRequiredDataItem = 2,
		ActivityAlreadyDone = 3,
		PermissionsError = 4,
		JSONContractError = 5
	}
}

