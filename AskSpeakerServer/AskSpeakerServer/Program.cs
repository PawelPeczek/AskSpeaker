using System;
using System.Data;
using MySql.Data.MySqlClient;

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
		}
	}
}
