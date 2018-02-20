using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Bidirectional {
	public class QuestionMergeMessage : MessagePrototype {

		public QuestionMergeMessage() {
			Message = AdminRequestTypes.QuestionMerge.GetRequestString(); 	
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

