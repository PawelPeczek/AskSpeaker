using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.AdministratorMessages.Bidirectional {
	public class QuestionMergeMessage {
		
		public string Message {
			get;
		} = AdminRequestTypes.QuestionMerge.GetRequestString(); 

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

