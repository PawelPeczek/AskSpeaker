using System;

namespace AskSpeakerServer.BackEnd.AdministratorRequests {
	public interface RequestMakerFactory {
		RequestMaker MakeRequest (AdminRequestTypes requestType);
	}
}

