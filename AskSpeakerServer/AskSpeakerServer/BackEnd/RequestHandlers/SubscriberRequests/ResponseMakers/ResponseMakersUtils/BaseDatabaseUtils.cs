using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using System.Collections.Generic;

namespace AskSpeakerServer.BackEnd.RequestHandlers.SubscriberRequests.ResponseMakers.ResponseMakersUtils {
  public abstract class BaseDatabaseUtils {

    protected string Hash;

    public BaseDatabaseUtils(string hash) {
      Hash = hash;
    }

    protected Events FetchEventWithGivenHash(AskSpeakerContext ctx, string hash) {
      Events result = (from e in ctx.Events
                       where e.EventHash == hash
                       select e).FirstOrDefault();
      if(result == null)
        throw new KeyNotFoundException("Event with given hash doesn't exist.");
      return result;
    }

    protected Questions FetchRequestedQuestion(AskSpeakerContext ctx, int questionID) {
      Questions result = (from q in ctx.Questions
                          where q.Anulled == false &&
                          q.QuestionID == questionID &&
                          q.Event.EventHash == Hash
                          select q).FirstOrDefault();
      if(result == null)
        throw new KeyNotFoundException("There is no such question at the selected event.");
      return result;
    }
  }
}
