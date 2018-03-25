using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionMergeBroadcast : QuestionBroadcast {
		
		public QuestionMergeBroadcast() {
			Broadcast = AdminRequestTypes.QuestionMerge.GetRequestString(); 	
		}

		public int MasterID { 
			get;
			set;
		}

		public int SlaveID {
			get;
			set;
		}
	}
}

