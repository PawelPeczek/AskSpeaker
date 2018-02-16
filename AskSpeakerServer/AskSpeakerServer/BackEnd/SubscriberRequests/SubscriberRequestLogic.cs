using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;

namespace AskSpeakerServer {
	public class SubscriberRequestLogic {
		public static string GetQuestionsJSON(string path){
			string result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events choseenEvent = 
					(from e in ctx.Events
					 where e.EventHash == path
					 select e
					).FirstOrDefault ();
				if (choseenEvent == null) {
					result = 
				}
			}
		}
	}
}

