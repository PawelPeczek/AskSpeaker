using System;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.EntityFramework.Entities;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AskSpeakerServer.BackEnd.Initialization {
  public static class SystemInit {
    public static void CreateRootUser() {
      using(AskSpeakerContext ctx = new AskSpeakerContext()) {
        Users newRoot = new Users();
        newRoot.UserName = "root";
        newRoot.Active = true;
        newRoot.UserRole = (from ur in ctx.UserRoles
                            where ur.RoleName == "SuperAdmin"
                            select ur).FirstOrDefault();
        if(newRoot.UserRole == null)
          throw new ApplicationException("DB Error!");
        SHA256 SHAEncryptor = SHA256Managed.Create();
        newRoot.Password =
          SHAEncryptor.ComputeHash(Encoding.Unicode.GetBytes("zaq1@WSX"));
        ctx.Users.Add(newRoot);
        ctx.SaveChanges();
      }
    }
  }
}
