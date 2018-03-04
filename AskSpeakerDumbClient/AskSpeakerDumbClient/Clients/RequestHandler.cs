using System;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerDumbClient.Clients {
	public abstract class RequestHandler <T> {
		protected T SelectedType;
		protected GeneralDialog<T> Dialog;

		public abstract BaseRequest PrepareRequest ();
	}
}

