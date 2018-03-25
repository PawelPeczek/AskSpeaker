using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class EventsListRequest : BaseRequest {

		public EventsListRequest () {
			Request = AdminRequestTypes.EventsInfoRenew.GetRequestString ();
		}

	}
}

