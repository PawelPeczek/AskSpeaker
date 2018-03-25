using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using System.Collections.Generic;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Responses;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;

namespace AskSpeakerServer.BackEnd.RequestHandlers.AdministratorRequests.ResponseMakers.ResponseMakersUtils {
  public class BasicDatabaseUtils {

    protected IDictionary<Object, Object> Credentials;

    public BasicDatabaseUtils(IDictionary<Object, Object> credentials) {
      Console.WriteLine("AdminRequestLogic ctor");
      Credentials = credentials;
      if(!AdminAuthenticationModule.IsUserStillActive(credentials))
        throw new UnauthorizedAccessException("User account was deleted.");
      if(AdminAuthenticationModule.HasPasswordForUserChanged(credentials))
        throw new PasswordHasChangedException("Password for user has changed during this session.");
    }

    public SuPermissionsCheckResponse CheckSuPermistions(SuPermissionsCheckRequest request) {
      Console.WriteLine("CheckSuPermistions()");
      SuPermissionsCheckResponse result = new SuPermissionsCheckResponse();
      result.PermissionsGranted = AdminAuthenticationModule.IsUserSuperAdmin(Credentials);
      result.PrepareToSend(request.RequestID);
      return result;
    }

    protected Questions FetchActiveQuestionWithGivenID(AskSpeakerContext ctx, int QuestionID) {
      Questions question = (from q in ctx.Questions
                            where q.Anulled == false &&
                                  q.QuestionID == QuestionID
                            select q).FirstOrDefault();
      if(question == null)
        throw new KeyNotFoundException($"There is no question with such ID ({QuestionID})" +
                                       " or the question is closed.");
      if(!AdminAuthenticationModule.IsPermissionToEventModifGranted(question.Event.UserID, Credentials))
        throw new UnauthorizedAccessException("Only SuperAdmin can cancell a quetsion assigned to " +
                                              "event hosted by another user.");
      return question;
    }

    protected Events FetchEventWithGivenHash(AskSpeakerContext ctx, string hash) {
      Events result =
        (from e in ctx.Events
         where e.EventHash == hash
         select e).FirstOrDefault();
      if(result == null)
        throw new KeyNotFoundException("There is no event with such hash.");
      if(!AdminAuthenticationModule.IsPermissionToEventModifGranted(result.UserID, Credentials))
        throw new UnauthorizedAccessException("Only SuperAdmin can modify event hosted by another user.");
      return result;
    }

    protected Events FetchEventWithGivenID(AskSpeakerContext ctx, int EventID) {
      Events result = (from e in ctx.Events
                       where e.EventID == EventID
                       select e).FirstOrDefault();
      if(result == null)
        throw new KeyNotFoundException("There is no event with such ID.");
      if(!AdminAuthenticationModule.IsPermissionToEventModifGranted(result.UserID, Credentials))
        throw new UnauthorizedAccessException("Only SuperAdmin can modify event hosted by another user.");
      return result;
    }

    protected Users FetchUserWithGivenID(AskSpeakerContext ctx, int UserID) {
      Users user;
      user = (from u in ctx.Users
              where u.UserID == UserID &&
                    u.Active == true
              select u).FirstOrDefault();
      if(user == null)
        throw new KeyNotFoundException("No user found.");
      return user;
    }

    protected Users FetchUserWithGivenUsername(AskSpeakerContext ctx, string userName) {
      Users user;
      user = (from u in ctx.Users
              where u.UserName == userName &&
              u.Active == true
              select u).FirstOrDefault();
      if(user == null)
        throw new KeyNotFoundException("No user found.");
      return user;
    }

    protected bool IsEventWithGivenHashExists(AskSpeakerContext ctx, string hash) {
      return (from e in ctx.Events
              where e.EventHash == hash
              select e).Any();
    }

    protected void ChangeEventsOwnerShip(AskSpeakerContext ctx, Users user, int newOwnerID) {
      Users newOwner = FetchUserWithGivenID(ctx, newOwnerID);
      if(newOwner == null)
        throw new KeyNotFoundException("There is no such active user.");
      foreach(Events userEvent in user.Events) {
        userEvent.User = newOwner;
      }
    }    
  }
}
