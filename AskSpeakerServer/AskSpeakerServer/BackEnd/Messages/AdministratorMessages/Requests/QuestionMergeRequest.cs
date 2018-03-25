using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Requests;
using AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests;

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

