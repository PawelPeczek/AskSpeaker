using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast {
	public class QuestionMergeBroadcast : BroadcastPrototype {
		
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

