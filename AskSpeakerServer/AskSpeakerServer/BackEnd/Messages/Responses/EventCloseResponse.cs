using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Responses {
	public class EventCloseResponse {
		
		public string Response {
			get;
		} = AdminRequestTypes.EventClose.GetRequestString();

		public int EventID {
			get;
			set;
		}
	}
}

