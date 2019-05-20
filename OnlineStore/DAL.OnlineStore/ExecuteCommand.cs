using System.Data.SqlClient;

namespace DAL.OnlineStore
{
	public abstract class ExecuteCommandBase
	{
		private readonly string _connectionString;
		protected ExecuteCommandBase(string connectionString)
		{
			_connectionString = connectionString;
		}

		protected object ExecuteCommand(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			object commandResult;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{

				connection.Open();

				SqlCommand command = new SqlCommand(storedProcedureName, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};


				foreach (SqlParameter sqlParameter in sqlParameters)
					command.Parameters.Add(sqlParameter);

				commandResult = command.ExecuteScalar();
				// если нам не надо возвращать id
				//var result = command.ExecuteNonQuery();

				connection.Close();
			}

			return commandResult;
		}

	}
}
