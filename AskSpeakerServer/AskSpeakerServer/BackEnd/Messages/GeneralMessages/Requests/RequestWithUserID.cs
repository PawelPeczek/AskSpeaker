using System;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests {
	public abstract class RequestWithUserID : BaseRequest {
		public int UserID {
			get;
			set;
		}
	}
}

