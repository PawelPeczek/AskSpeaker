using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests  {
	public class RequestWithEventID : BaseRequest {
		public int EventID {
			get;
			set;
		}
	}
}

