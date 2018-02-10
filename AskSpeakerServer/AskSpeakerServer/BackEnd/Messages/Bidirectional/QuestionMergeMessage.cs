using System;

namespace AskSpeakerServer.BackEnd.Messages.Bidirectional {
	public class QuestionMergeMessage {
		
		public string Message {
			get;
		} = AdminRequestTypes.QuestionCancell.GetRequestString(); 

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

