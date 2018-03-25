using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using AskSpeakerServer.BackEnd.Messages.AdministratorMessages.Requests;
using AskSpeakerServer.BackEnd.Messages.GeneralMessages.Responses;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;

namespace AskSpeakerServer.BackEnd.AdministratorRequests.ResponseMakers.ResponseMakersUtils {
  public class UsersUtils : BasicDatabaseUtils {

    public UsersUtils(IDictionary<Object, Object> credentials) : base(credentials) { }

    public OperationResponse CreateUser(UserCreateRequest request) {
      if(!AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
        throw new UnauthorizedAccessException("SuperUser access required.");
      OperationResponse result = new OperationResponse();
      result.Response = AdminRequestTypes.UserCreate.GetRequestString();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        SHA256 SHAEncryptor = SHA256.Create();
        byte[] encryptedPasswd = SHAEncryptor.ComputeHash(Encoding.Unicode.GetBytes(request.Password));
        Users user = new Users();
        user.UserName = request.UserName;
        user.Password = encryptedPasswd;
        // Not catching exception cause the lack of "Admin" UserRole 
        // indicates serious server-side error and the process should be terminated.
        UserRoles role = (from ur in ctx.UserRoles
                          where ur.RoleName == "Admin"
                          select ur).First();
        user.UserRole = role;
        ctx.Users.Add(user);
        try {
          ctx.SaveChanges();
        } catch(DataException ex) {
          throw new DataException($"Error while creating user. Details:\n {ex.Message}");
        }
        result.PrepareToSend(request.RequestID, AdminRequestTypes.UserCreate.GetRequestString());
      }
      return result;
    }

    public OperationResponse DeactivateUser(UserDeleteRequest request) {
      if(!AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
        throw new UnauthorizedAccessException("SuperUser access required.");
      OperationResponse result = new OperationResponse();
      result.Response = AdminRequestTypes.UserDelete.GetRequestString();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Users user =
          (from u in ctx.Users
           where u.UserID == request.UserID
           select u).FirstOrDefault();
        if(user == null)
          throw new ApplicationException("User does not exist.");
        if(user.Active == false) {
          throw new ApplicationException("User already deleted.");
        } else {
          user.Active = false;
          ChangeEventsOwnerShip(ctx, user, request.NewEventOwnerID);
          try {
            ctx.SaveChanges();
          } catch(DataException ex) {
            throw new DataException($"Error while deleting user. Details:\n {ex.Message}");
          }
        }
        result.PrepareToSend(request.RequestID, AdminRequestTypes.UserDelete.GetRequestString());
      }
      return result;
    }

    public OperationResponse ChangePassword(PasswordChangeRequest request) {
      OperationResponse result = new OperationResponse();
      result.Response = AdminRequestTypes.PasswordChange.GetRequestString();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Users user = FetchUserWithGivenID(ctx, (int)Credentials["UserID"]);
        Console.WriteLine("User that was fetched: " + user.UserName);
        SHA256 SHAEncryptor = SHA256Managed.Create();
        byte[] encryptedOldPasswd = SHAEncryptor.ComputeHash(Encoding.Unicode.GetBytes(request.OldPassword));
        if(user.Password.SequenceEqual(encryptedOldPasswd)) {
          byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash(Encoding.Unicode.GetBytes(request.NewPassword));
          user.Password = encryptedNewPasswd;
          ctx.SaveChanges();
          Credentials["PasswordChanged"] = true;
        } else {
          throw new ArgumentException("Wrong origin password.");
        }
        result.PrepareToSend(request.RequestID, AdminRequestTypes.PasswordChange.GetRequestString());
      }
      return result;
    }


    public SuPasswdChangeResponse ChangePasswordWithSuPermissions(PasswordChangeSuRequest request) {
      if(!AdminAuthenticationModule.IsUserSuperAdmin(Credentials))
        throw new UnauthorizedAccessException("SuperUser access required.");
      SuPasswdChangeResponse result = new SuPasswdChangeResponse();
      result.Response = AdminRequestTypes.PasswordChangeWithSu.GetRequestString();
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Users user = FetchUserWithGivenID(ctx, request.UserID);
        Console.WriteLine($"New password: {request.NewPassword} for user {user.UserName}");
        SHA256 SHAEncryptor = SHA256Managed.Create();
        byte[] encryptedNewPasswd = SHAEncryptor.ComputeHash(Encoding.Unicode.GetBytes(request.NewPassword));
        user.Password = encryptedNewPasswd;
        ctx.SaveChanges();
        if((int)Credentials["UserID"] == request.UserID) Credentials["PasswordChanged"] = true;
        result.PrepareToSend(request.RequestID, AdminRequestTypes.PasswordChangeWithSu.GetRequestString());
        result.UserID = user.UserID;
      }
      return result;
    }
  }
}
