using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional {
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

