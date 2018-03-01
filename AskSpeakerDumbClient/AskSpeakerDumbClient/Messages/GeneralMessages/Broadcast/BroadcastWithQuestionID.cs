using System;
using AskSpeakerServer.BackEnd.Messages.Prototypes;

namespace AskSpeakerServer.BackEnd.Messages.GeneralMessages.Broadcast  {
	public abstract class BroadcastWIthQuestionID : BroadcastPrototype {
		public int QuestionID {
			get;
			set;
		}
	}
}