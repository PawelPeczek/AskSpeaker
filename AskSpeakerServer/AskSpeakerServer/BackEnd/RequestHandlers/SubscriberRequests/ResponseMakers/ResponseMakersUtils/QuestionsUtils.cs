using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.SubscriberMessages.Responses;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using AskSpeakerServer.Extensions;
using Newtonsoft.Json;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils {
  public class QuestionsUtils : BaseDatabaseUtils {

    public QuestionsUtils(string hash) : base(hash) {}

    public QuestionVoteBroadcast VoteQuestion(VoteQuestionRequest request) {
      QuestionVoteBroadcast result = new QuestionVoteBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Votes vote = new Votes();
        Questions question = FetchRequestedQuestion(ctx, request.QuestionID);
        vote.Question = question;
        vote.Value = request.VoteUp.ConvertVote();
        ctx.Votes.Add(vote);
        try {
          ctx.SaveChanges();
        } catch(DataException) {
          // Replacing error message that is not approptiate to client eyes.
          throw new DataException("Error while updating data.");
        }
        result.QuestionID = request.QuestionID;
        result.PrepareToSend(Hash);
      }
      return result;
    }

    public QuestionAddBroadcast AddQuestion(QuestionAddRequest request) {
      QuestionAddBroadcast result = new QuestionAddBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Questions question = new Questions();
        Events selectedEvent = FetchEventWithGivenHash(ctx, Hash);
        question.Event = selectedEvent;
        question.QuestionContent = request.QuestionContent;
        question.Anulled = false;
        ctx.Questions.Add(question);
        ctx.SaveChanges();
        result.Question = question;
        result.PrepareToSend(Hash);
      }
      return result;
    }

    public string GetQuestionsJSON(QuestionsRequest request) {
      string result;
      Console.WriteLine("GetQuestionsJSON()");
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Events chosenEvent = FetchEventWithGivenHash(ctx, Hash);
        ICollection<Questions> allQuestions = chosenEvent.Questions;
        Dictionary<Questions, List<Questions>> mergedTo = new Dictionary<Questions, List<Questions>>();
        HashSet<Questions> primaryQuestions = new HashSet<Questions>();
        Console.WriteLine("Before foreach");
        foreach(Questions question in allQuestions) {
          Console.WriteLine($"processed questionID: {question.QuestionID}");
          IQueryable<Votes> votesForQuestion =
                    (from v in ctx.Votes
                     where v.QuestionID == question.QuestionID
                     select v);
          if(votesForQuestion.Any())
            question.VotesSum = votesForQuestion.Sum(o => o.Value);
          else
            question.VotesSum = 0;
          Console.WriteLine($"q.voteSum: {question.VotesSum}");
          if(question.Merged == null) {
            primaryQuestions.Add(question);
          } else {
            if(!mergedTo.ContainsKey(question.Merged))
              mergedTo.Add(question.Merged, new List<Questions>());
            mergedTo[question.Merged].Add(question);
          }
        }
        Console.WriteLine("After foreach");
        CountVotesForPrimaryQuestions(allQuestions, mergedTo);
        Console.WriteLine("Preparing response");
        QuestionsListResponse response = new QuestionsListResponse();
        response.PrepareToSend(request.RequestID);
        response.Path = Hash;
        response.Questions = primaryQuestions;
        result = JsonConvert.SerializeObject(response);
      }
      return result;
    }

    private void CountVotesForPrimaryQuestions(ICollection<Questions> allQuestions,
      Dictionary<Questions, List<Questions>> mergedTo) {
      HashSet<Questions> alreadyMemorized = new HashSet<Questions>();
      foreach(Questions question in allQuestions) {
        CountVotesForSingleQuestion(question, alreadyMemorized, mergedTo);
      }
    }

    private void CountVotesForSingleQuestion(Questions question,
      HashSet<Questions> alreadyMemorized, Dictionary<Questions, List<Questions>> mergedTo) {
      if(!alreadyMemorized.Contains(question)) {
        if(mergedTo.ContainsKey(question)) {
          foreach(Questions questionFromMergedList in mergedTo[question]) {
            CountVotesForSingleQuestion(questionFromMergedList, alreadyMemorized, mergedTo);
            question.VotesSum += questionFromMergedList.VotesSum;
          }
        }
        alreadyMemorized.Add(question);
      }
    }

  }
}
