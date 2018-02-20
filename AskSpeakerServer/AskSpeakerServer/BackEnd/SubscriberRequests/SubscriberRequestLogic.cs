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


namespace AskSpeakerServer.BackEnd.SubscriberRequests {
	public class SubscriberRequestLogic {

		public static string GetQuestionsJSON(string hash, int RequestID = 0){
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
				response.SetCurrentTimestamp ();
				response.Response = SubscriberRequestTypes.QuestionsRequest.GetRequestString ();
				response.RequestID = RequestID;
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
				throw new ApplicationException ("Event with given hash doesn't exist.");
			return result;
		}

		private string Hash;

		public SubscriberRequestLogic(string hash){
			Hash = hash;
		}

		public string ObtainQuestionsList(RenewQuestionsRequest request){
			return GetQuestionsJSON (Hash, request.RequestID);
		}

		public OperationResponse VoteQuestion(VoteQuestionRequest request){
			OperationResponse response = new OperationResponse ();
			response.RequestID = request.RequestID;
			response.Response = SubscriberRequestTypes.VoteRequest.GetRequestString();
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Votes vote = new Votes ();
				Questions question = (from q in ctx.Questions
									  where q.Anulled == false && 
									  q.QuestionID == request.QuestionID &&
									  q.Event.EventHash == Hash
									  select q).FirstOrDefault();
				if (question == null) {
					response.ErrorCause = "There is no such question at the selected event.";
					response.ErrorCode = ResponseCodes.CannotFindRequiredDataItem.GetResponseCodeInt ();
				}
				vote.Question = question;
				vote.Value = request.VoteUp.ConvertVote ();
				ctx.Votes.Add (vote);
				try {
					ctx.SaveChanges ();
				} catch(Exception ex) {
					response.ErrorCause = "Error while saving vote.";
					response.ErrorCode = ResponseCodes.DataConstraintViolated.GetResponseCodeInt ();
				}
				response.SetCurrentTimestamp ();
			}
			return response;
		}

		public QuestionAddResponse AddQuestion(QuestionAddRequest request){
			QuestionAddResponse result = new QuestionAddResponse ();
			result.RequestID = request.RequestID;
			using (AskSpeakerContext ctx = new AskSpeakerContext ()) {
				Questions question = new Questions ();
				Events selectedEvent = FetchEventWithGivenHash (ctx, Hash);
				question.Event = selectedEvent;
				question.QuestionContent = request.QuestionContent;
				question.Anulled = false;
				ctx.Questions.Add (question);
				try {
					ctx.SaveChanges();
					result.Question = question;
				} catch (Exception ex) {
					result.Question = null;
					result.ErrorCode = ResponseCodes.DataConstraintViolated.GetResponseCodeInt ();
					result.ErrorMessage = "Question to long (maximal length: 350 characters).";
				}
				result.SetCurrentTimestamp ();
			}
			return result;
		}


	}
}

