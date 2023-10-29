using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;

namespace TACSV
{
	public class DataManager
	{
		private static SqliteConnection dbConnection;
		public static void InitialiseDatabase()
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder();
			connectionStringBuilder.DataSource = Program.DBPath;
			dbConnection = new SqliteConnection(connectionStringBuilder.ToString());
			dbConnection.Open();
		}

		public static void CloseDatabase()
		{
			dbConnection.Close();
		}
	}
}
