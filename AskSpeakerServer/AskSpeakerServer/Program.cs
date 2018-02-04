using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.BackEnd;
using System.Security.Cryptography;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Security.Cryptography.X509Certificates;

namespace AskSpeakerServer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using(var ctx = new AskSpeakerContext()){
				var roles = from p in ctx.UserRoles select p;
				foreach (var r in roles) {
					Console.WriteLine($"{r.UserRoleID} - {r.RoleName}");
				}
//				SHA256 mySHA256 = SHA256Managed.Create();
//				Users user = new Users();
//				user.UserName = "DumbUser";
//				user.Password = mySHA256.ComputeHash (Encoding.Unicode.GetBytes("zaq1@WSX"));
//				UserRoles role = (from p in ctx.UserRoles
//				                  where p.RoleName.Equals ("Admin")
//				                  select p).FirstOrDefault ();
//				user.UserRole = role;
//				Events Event = new Events();
//				Event.EventHash = "111111";
//				Event.EventName = "Some event";
//				Event.User = user;
//				user.Events.Add(Event);
//				ctx.Users.Add (user);
//				ctx.Events.Add (Event);
//				ctx.SaveChanges();
			}

			WebSocketServer wss = new WebSocketServer ("ws://localhost:10000");
			wss.AddWebSocketService<ClientRequests> ("/ClientRequest");
			wss.Start ();
			Console.WriteLine ("Press any key to quit...");
			Console.ReadKey ();
			wss.Stop ();

		}
	}
}
