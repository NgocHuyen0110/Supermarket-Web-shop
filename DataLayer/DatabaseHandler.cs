using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer
{
	public abstract class DatabaseHandler
	{
		private readonly SqlConnection _sqlConnection;

		protected DatabaseHandler()
		{
			string connectionInfo =
			//@"Data Source=mssqlstud.fhict.local;
			//Initial Catalog=dbi475867_synthesis;
			//Persist Security Info=True;
			//User ID=dbi475867_synthesis;
			//Password=***********;
			//trusted_connection=true;
			//encrypt=false;
			//Integrated Security=true";
			@"Server=mssqlstud.fhict.local;Database=dbi475867_synthesis;User Id=dbi475867_synthesis;Password=Chau11041963;
			Encrypt=True;TrustServerCertificate=True;";
			this._sqlConnection = new SqlConnection(connectionInfo);
		}

		protected SqlConnection GetDbConnection()
		{
			return this._sqlConnection;
		}

		protected long ExecuteCount(SqlCommand sqlCountCommand)
		{
			try
			{
				this._sqlConnection.Open();
				long count = (long)sqlCountCommand.ExecuteScalar();

				return count;
			}
			finally
			{
				this._sqlConnection.Close();
			}
		}

		protected int ExecuteNonQuery(SqlCommand sqlNonQueryCommand)
		{
			try
			{
				this._sqlConnection.Open();
				int numberAffectedRows = sqlNonQueryCommand.ExecuteNonQuery();

				return numberAffectedRows;
			}
			finally
			{
				this._sqlConnection.Close();
			}
		}

		protected object ExecuteScalar(SqlCommand sqlScalarCommand)
		{
			try
			{
				this._sqlConnection.Open();
				object numberAffectedRows = sqlScalarCommand.ExecuteScalar();

				return numberAffectedRows;
			}
			finally
			{
				this._sqlConnection.Close();
			}
		}

		protected SqlDataReader? OpenExecuteReader(SqlCommand sqlReaderCommand)
		{
			this._sqlConnection.Open();
			SqlDataReader reader = sqlReaderCommand.ExecuteReader();

			return reader;
		}

		protected void CloseExecuteReader(SqlDataReader? reader)
		{
			if (reader != null)
				reader.Close();

			if (_sqlConnection.State == ConnectionState.Open)
				this._sqlConnection.Close();
		}
	}
}
