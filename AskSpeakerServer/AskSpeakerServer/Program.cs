using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using AskSpeakerServer.EntityFramework;
using System.Security.Cryptography;
using System.Text;

namespace AskSpeakerServer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string connectionString =
				"Server=localhost;" +
				"Database=AskSpeakerDB;" +
				"User ID=root;" +
				"Password=zaq1@WSX;" +
				"Pooling=false";
			IDbConnection dbcon;
			dbcon = new MySqlConnection(connectionString);
			dbcon.Open();
			IDbCommand dbcmd = dbcon.CreateCommand();
			// requires a table to be created named employee
			// with columns firstname and lastname
			// such as,
			//        CREATE TABLE employee (
			//           firstname varchar(32),
			//           lastname varchar(32));
			string sql =
				"SELECT * " +
				"FROM UserRoles";
			dbcmd.CommandText = sql;
			IDataReader reader = dbcmd.ExecuteReader();
			while(reader.Read()) {
				int FirstName = (int) reader["UserRoleID"];
				string LastName = (string) reader["RoleName"];
				Console.WriteLine("Data: " +
					FirstName + " " + LastName);
			}
			// clean up
			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbcon.Close();
			dbcon = null;
			using(var ctx = new AskSpeakerContext()){
				var roles = from p in ctx.UserRoles select p;
				foreach (var r in roles) {
					Console.WriteLine($"{r.UserRoleID} - {r.RoleName}");
				}
				SHA256 mySHA256 = SHA256Managed.Create();
				Users user = new Users();
				user.UserName = "DumbUser";
				user.Password = mySHA256.ComputeHash (Encoding.Unicode.GetBytes("zaq1@WSX"));
				UserRoles role = (from p in ctx.UserRoles
				                  where p.RoleName.Equals ("Admin")
				                  select p).FirstOrDefault ();
				user.UserRole = role;
				Events Event = new Events();
				Event.EventHash = "111111";
				Event.EventName = "Some event";
				Event.User = user;
				user.Events.Add(Event);
				ctx.Users.Add (user);
				ctx.Events.Add (Event);
				ctx.SaveChanges();
			}
		}
	}
}
