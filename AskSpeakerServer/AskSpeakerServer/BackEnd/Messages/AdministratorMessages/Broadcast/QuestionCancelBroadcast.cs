using AskSpeakerServer.BackEnd.Messages.Prototypes;
using Newtonsoft.Json;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionCancelBroadcast : BroadcastWIthQuestionID {

		public QuestionCancelBroadcast() {
			Broadcast = AdminRequestTypes.QuestionCancell.GetRequestString();
		}
	
	}
}

