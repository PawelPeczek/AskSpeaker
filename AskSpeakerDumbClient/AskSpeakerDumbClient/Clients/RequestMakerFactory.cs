using System;

namespace AskSpeakerDumbClient.Clients {
	public interface RequestMakerFactory<T> {
		RequestMaker<T> MakeRequest (T requestType);
	}
}

