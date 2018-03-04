using System;
using AskSpeakerServer.BackEnd.AdministratorRequests.RequestImplementations;
using AskSpeakerDumbClient.Clients;

namespace AskSpeakerServer.BackEnd.AdministratorRequests  {
	public class AdminRequestMakerFactoryImpl : RequestMakerFactory<AdminRequestTypes> {

		public RequestMaker<AdminRequestTypes> MakeRequest (AdminRequestTypes requestType) {
			RequestMaker<AdminRequestTypes> result;
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
				case AdminRequestTypes.EventsInfoRenew:
					result = new EventsInfoRenevRequestMaker ();
					break;
				case AdminRequestTypes.PasswordChange:
					result = new PasswordChangeRequestMaker ();
					break;
				case AdminRequestTypes.PasswordChangeWithSu:
					result = new PasswordChangeWithSuRequestMaker ();
					break;
				case AdminRequestTypes.QuestionCancell:
					result = new QuestionCancelRequestMaker ();
					break;
				case AdminRequestTypes.QuestionEdit:
					result = new QuestionEditRequestMaker ();
					break;
				case AdminRequestTypes.QuestionMerge:
					result = new QuestionMergeRequestMaker ();
					break;
				case AdminRequestTypes.SuPermissionsCheck:
					result = new SuPermissionsCheckRequestMaker ();
					break;
				case AdminRequestTypes.UserCreate:
					result = new UserCreateRequestMaker ();
					break;
				case AdminRequestTypes.UserDelete:
					result = new UserDeleteRequestMaker ();
					break;
				default:
					throw new NotImplementedException ("Not jet implemented");
			}
			return result;
		}

	}
}

