using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserAdmittanceRepository : ExecuteCommandBase, IRepositoryUserAdmittance
	{
		#region Stored procedure names

		const string SpSelectByLoginPassword = @"UserAdmittance_SelectByLoginPassword";
		const string SpSelectByLogin = @"UserAdmittance_SelectByLogin";
		const string SpInsert = @"UserAdmittance_Insert";
		const string SpDelete = @"UserAdmittance_Delete";
		const string SpUpdate = @"UserAdmittance_Update";
		const string SpSelectById = @"UserAdmittance_SelectById";
		const string SpSelectAll = @"UserAdmittance_SelectAll";

		#endregion

		private readonly string _connectionString;

		public UserAdmittanceRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		public UserAdmittance GetUserAdmittance(string login, string passwordHash)
		{

			var loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = login
			};
			var passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = passwordHash
			};

			var userAccess = ReadUserAccess(SpSelectByLoginPassword, loginParam, passwordHashParam);

			return userAccess;


		}
		public bool LoginUserIsUnique(string login)
		{

			var loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = login
			};

			var userAccess = ReadUserAccess(SpSelectByLogin, loginParam);

			if (userAccess == null)
				return true;

			return false;


		}


		public int? Insert(UserAdmittance item)
		{

			var loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = item.Login
			};
			var passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = item.PasswordHash
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.UserStatus.GetStatusName()
			};
			var roleParam = new SqlParameter
			{
				ParameterName = "@Role",
				Value = item.UserRole.GetRoleName()
			};

			var resultCommand = ExecuteCommand(SpInsert, loginParam, passwordHashParam, statusParam, roleParam);

			int? result = null;

			if (resultCommand != null)
			{
				decimal lastId = (decimal)resultCommand;
				result = Decimal.ToInt32(lastId);
			}

			return result;


		}
		public bool Update(UserAdmittance item)
		{

			if (!item.IdEntity.HasValue)
				throw new ArgumentException($"Parameter{nameof(item.IdEntity)} must be not empty", nameof(item.IdEntity));

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			var loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = item.Login
			};
			var passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = item.PasswordHash
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.UserStatus.GetStatusName()
			};
			var roleParam = new SqlParameter
			{
				ParameterName = "@Role",
				Value = item.UserRole.GetRoleName()
			};

			var resultCommand = ExecuteCommand(SpUpdate, idParam, loginParam, passwordHashParam, statusParam, roleParam);


			return resultCommand != null && (int)resultCommand == 1;



		}
		public bool Delete(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(UserAdmittanceRepository)}-{nameof(Delete)}: id must be more 0");


			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			object resultCommand = ExecuteCommand(SpDelete, idParam);

			return resultCommand != null && (int)resultCommand == 1;


		}


		public UserAdmittance SelectById(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(UserAdmittanceRepository)}-{nameof(SelectById)}: id must be more 0");


			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			var userAccess = ReadUserAccess(SpSelectById, idParam);

			return userAccess;


		}
		public List<UserAdmittance> SelectAll()
		{

			var userAccesses = ReadUserAccesses(SpSelectAll);

			return userAccesses;


		}
		public List<UserAdmittance> Find(Func<UserAdmittance, bool> predicate)
		{
			var listTemp = ReadUserAccesses(SpSelectAll);

			var userAccesses = listTemp.Where(predicate);

			return userAccesses.ToList();

		}


		private List<UserAdmittance> ReadUserAccesses(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			List<UserAdmittance> list = new List<UserAdmittance>();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(storedProcedureName, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};

				foreach (SqlParameter sqlParameter in sqlParameters)
					command.Parameters.Add(sqlParameter);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						int readId = reader.GetInt32(0);
						string readLogin = reader.GetString(1);
						string readPassword = reader.GetString(2);
						string readStatus = reader.GetString(3);
						string readRole = reader.GetString(4);

						UserAdmittance userAdmittance = new UserAdmittance(readId, readLogin, readPassword, readStatus, readRole);

						list.Add(userAdmittance);
					}
				}


			}

			return list;
		}
		private UserAdmittance ReadUserAccess(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(storedProcedureName, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};

				foreach (SqlParameter sqlParameter in sqlParameters)
					command.Parameters.Add(sqlParameter);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						int readId = reader.GetInt32(0);
						string readLogin = reader.GetString(1);
						string readPassword = reader.GetString(2);
						string readStatus = reader.GetString(3);
						string readRole = reader.GetString(4);

						var userAccess = new UserAdmittance(readId, readLogin, readPassword, readStatus, readRole);

						return userAccess;
					}
				}
			}

			return null;


		}




		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}
	}
}
