using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations;

namespace AskSpeakerServer.BackEnd.AdministratorRequests  {
	public class RequestMakerFactoryImpl : RequestMakerFactory {

		public RequestMaker MakeRequest (AdminRequestTypes requestType) {
			RequestMaker result;
			switch (requestType) {
				case AdminRequestTypes.EventChangeOwnership:
					result = new EventChangeOwnershipRequestMaker ();
					break;
				case AdminRequestTypes.EventClose:
					result = new EventCloseRequestMaker ();
					break;
				case AdminRequestTypes.EventReOpen:
					result = new EventReOpenMaker ();
					break;
				case AdminRequestTypes.EventCreate:
					result = new EventCreateRequestMaker ();
					break;
				case AdminRequestTypes.EventEdit:
					result = new EventEditRequestMaker ();
					break;
				default:
					throw new NotImplementedException ("Not jet implemented");
			}
			return result;
		}

	}
}

