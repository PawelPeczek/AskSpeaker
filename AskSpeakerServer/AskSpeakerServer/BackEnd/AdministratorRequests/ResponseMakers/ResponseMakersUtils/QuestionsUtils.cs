using System;
using System.Collections.Generic;
using System.Data;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Broadcast;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils {
  public class QuestionsUtils : BasicDatabaseUtils {
    
    public QuestionsUtils(IDictionary<Object, Object> credentials) : base(credentials) { }

    public QuestionCancelBroadcast CancellQuestion(QuestionCancelRequest request) {
      QuestionCancelBroadcast result = new QuestionCancelBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Questions question = FetchActiveQuestionWithGivenID(ctx, request.QuestionID);
        if(!question.Anulled) {
          question.Anulled = true;
          ctx.SaveChanges();
        } else
          throw new ApplicationException("Question already cancelled.");
        result.QuestionID = request.QuestionID;
        result.PrepareToSend(question.Event.EventHash);
      }
      return result;
    }

    public QuestionMergeBroadcast MergeQuestions(QuestionMergeRequest request) {
      QuestionMergeBroadcast result = new QuestionMergeBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Questions master = FetchActiveQuestionWithGivenID(ctx, request.MasterID);
        Questions slave = FetchActiveQuestionWithGivenID(ctx, request.SlaveID);
        if(master.EventID != slave.EventID)
          throw new InvalidOperationException("Cannot merge questions associated to different events.");
        if(master.QuestionID == slave.QuestionID)
          throw new InvalidOperationException("Cannot merge question with itself.");
        slave.Merged = master;
        ctx.SaveChanges();
        result.MasterID = request.MasterID;
        result.SlaveID = request.SlaveID;
        result.PrepareToSend(master.Event.EventHash);
      }
      return result;
    }

    public QuestionEditBroadcast EditQuestion(QuestionEditRequest request) {
      QuestionEditBroadcast result = new QuestionEditBroadcast();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Questions origin = FetchActiveQuestionWithGivenID(ctx, request.QuestionID);
        origin.QuestionContent = request.NewQuestionContent;
        try {
          ctx.SaveChanges();
        } catch(DataException ex) {
          throw new DataException($"Broken JSON Question-serialize contract. Details: {ex.Message}");
        }
        result.NewQuestionContent = request.NewQuestionContent;
        result.QuestionID = request.QuestionID;
        result.PrepareToSend(origin.Event.EventHash);
      }
      return result;
    }
  }
}
