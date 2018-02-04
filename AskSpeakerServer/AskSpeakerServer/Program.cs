using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using AskSpeakerServer.EntityFramework;
using AskSpeakerServer.BackEnd;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SuperSocket.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

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
			UserRequests server = new UserRequests ();
			server.NewMessageReceived += async (session, value) => {
				var ses = session.AppServer.GetAllSessions();
				foreach(var s in ses){
					await Task.Run(() => s.Send($"Echo from host {session.SessionID}:\n{value}"));
				};
				await Task.Run(() => session.Send("Don't worry - your message was send!"));
			
			};
			server.NewSessionConnected += async (WebSocketSession session) => {
				await Task.Run(() => session.Send("You're logged in!"));
				Console.WriteLine("Client connected!");
			};
			server.SessionClosed += delegate(WebSocketSession session, SuperSocket.SocketBase.CloseReason value) {
				Console.WriteLine("Sesion closed!");
			};
			server.Start ();
			Console.ReadKey ();
			server.Stop ();
		}
	}
}
