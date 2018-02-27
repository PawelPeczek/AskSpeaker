using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.Extensions;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages;
using System.Data;


namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestLogic {

		public static string GetQuestionsJSON(string hash, int requestID = -1, bool extendedList = false){
			string result;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Events chosenEvent = FetchEventWithGivenHash (ctx, hash);
				ICollection<Questions> allQuestions = chosenEvent.Questions;
				Dictionary<Questions, List<Questions>> mergedTo = new Dictionary<Questions, List<Questions>> ();
				HashSet<Questions> primaryQuestions = new HashSet<Questions> ();
				foreach (Questions question in allQuestions) {
					question.VotesSum = (from v in ctx.Votes 
										 where v.QuestionID == question.QuestionID 
										 select v).Sum(o => o.Value);
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
				response.PrepareToSend (requestID);
				response.Path = hash;
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
						CountVotesForSingleQuestion (questionFromMergedList, alreadyMemorized, mergedTo);
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
				throw new KeyNotFoundException ("Event with given hash doesn't exist.");
			return result;
		}

		private string Hash;

		public SubscriberRequestLogic(string hash){
			Hash = hash;
		}

		public string ObtainQuestionsList(RenewQuestionsRequest request){
			return GetQuestionsJSON (Hash, request.RequestID);
		}
			
		public QuestionVoteBroadcast VoteQuestion(VoteQuestionRequest request){
			QuestionVoteBroadcast result = new QuestionVoteBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Votes vote = new Votes ();
				Questions question = FetchRequestedQuestion (ctx, request.QuestionID);
				vote.Question = question;
				vote.Value = request.VoteUp.ConvertVote ();
				ctx.Votes.Add (vote);
				try {
					ctx.SaveChanges ();
				} catch (DataException) {
					// Replacing error message that is not approptiate to client eyes.
					throw new DataException ("Error while updating data.");
				}
				result.PrepareToSend (Hash);
			}
			return result;
		}

		public QuestionAddBroadcast AddQuestion(QuestionAddRequest request){
			QuestionAddBroadcast result = new QuestionAddBroadcast ();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions question = new Questions ();
				Events selectedEvent = FetchEventWithGivenHash (ctx, Hash);
				question.Event = selectedEvent;
				question.QuestionContent = request.QuestionContent;
				question.Anulled = false;
				ctx.Questions.Add (question);
				ctx.SaveChanges();
				result.Question = question;
				result.PrepareToSend (Hash);
			}
			return result;
		}


		private Questions FetchRequestedQuestion(AskSpeakerContext ctx,  int questionID){
			Questions result = (from q in ctx.Questions
				where q.Anulled == false && 
				q.QuestionID == questionID &&
				q.Event.EventHash == Hash
				select q).FirstOrDefault();
			if (result == null) 
				throw new KeyNotFoundException ("There is no such question at the selected event.");
			return result;
		}


	}
}

