using System;
using System.Data;
using System.Data.SqlClient;

namespace WebFileShare
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Database
	{
		private static SqlConnection connection;

		private static SqlConnection GetConnection()
		{
			if (connection == null)
			{
				connection = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["dbConnectionString"].ToString());
			}

			if (!(connection.State == ConnectionState.Open))
			{
				connection.Open();
			}
            
			return connection;
		}
        
		public static SqlDataReader OpenQuery(string cmdText)
		{
			return (new SqlCommand(cmdText, GetConnection())).ExecuteReader();
		}

		public static void ExecQuery(string cmdText)
		{
			(new SqlCommand(cmdText, GetConnection())).ExecuteNonQuery();
		}
	}
}
