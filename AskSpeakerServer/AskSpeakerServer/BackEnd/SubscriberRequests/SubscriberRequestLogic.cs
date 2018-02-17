using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.SubscriberMessages.Responses;

namespace AskSpeakerServer {
	public class SubscriberRequestLogic {

		public static string GetQuestionsJSON(string path){
			string result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events choseenEvent = FetchEventWithGivenHash (ctx, path);
				ICollection<Questions> allQuestions = choseenEvent.Questions;
				Dictionary<Questions, List<Questions>> mergedTo = new Dictionary<Questions, List<Questions>> ();
				HashSet<Questions> primaryQuestions = new HashSet<Questions> ();
				foreach (Questions question in allQuestions) {
					question.VotesSum = question.Votes.Sum(o => o.Value);
					if (question.Merged == null) {
						primaryQuestions.Add (question);
					} else {
						if (!mergedTo.ContainsKey (question.Merged))
							mergedTo.Add (question.Merged, new List<Questions> ());
						mergedTo [question.Merged].Add (question);
					}
				}
				CountVotesForPrimaryQuestions (allQuestions, mergedTo);
				QuestionsListResponse response = new QuestionsListResponse ();
				response.Path = path;
				response.Questions = primaryQuestions;
				result = JsonConvert.SerializeObject (response);
			}
			return result;
		}

		private static void CountVotesForPrimaryQuestions(ICollection<Questions> allQuestions,
			Dictionary<Questions, List<Questions>> mergedTo){
			HashSet<Questions> alreadyMemorized = new HashSet<Questions> ();
			foreach (Questions question in allQuestions) {
				CountVotesForSingleQuestion (question, alreadyMemorized, mergedTo);
			}			
		}

		private static void CountVotesForSingleQuestion(Questions question, 
			HashSet<Questions> alreadyMemorized, Dictionary<Questions, List<Questions>> mergedTo){
			if (!alreadyMemorized.Contains (question)) {
				if (mergedTo.ContainsKey (question)){
					foreach (Questions questionFromMergedList in mergedTo[question]) {
						CountVotesForSingleQuestion (question, alreadyMemorized, mergedTo);
						question.VotesSum += questionFromMergedList.VotesSum;
					}
				}
				alreadyMemorized.Add (question);
			}
		}

		private static Events FetchEventWithGivenHash(AskSpeakerContext ctx, string hash){
			Events result = (from e in ctx.Events
							 where e.EventHash == hash
							 select e).FirstOrDefault ();
			if (result == null)
				throw new ApplicationException ("Event with given hash doesn't exist.");
			return result;
		}
	}
}

