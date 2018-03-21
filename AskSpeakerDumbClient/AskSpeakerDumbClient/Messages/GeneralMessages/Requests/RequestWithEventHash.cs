using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests  {
	public class RequestWithEventHash : BaseRequest {
		public string EventHash {
			get;
			set;
		}
	}
}

