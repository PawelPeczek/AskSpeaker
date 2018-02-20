using System;
using AskSpeakerServer.BackEnd.AdministratorRequests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;

namespace AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests {
	public class QuestionMergeRequest : BaseRequest {

		public QuestionMergeRequest() {
			Request = AdminRequestTypes.QuestionMerge.GetRequestString(); 	
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

