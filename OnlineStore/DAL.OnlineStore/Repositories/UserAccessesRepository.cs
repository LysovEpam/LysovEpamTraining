using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserAccessRepository : ExecuteCommandBase, IRepositoryUserAccess
	{

		private readonly string _connectionString;

		public UserAccessRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}


		public UserAccess GetUserAccess(string login, string passwordHash)
		{
			UserAccess userAccessResult;

			// название процедуры
			string sqlExpression = @"UserAccess_SelectByLoginPassword";

			SqlParameter loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = login
			};
			SqlParameter passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = passwordHash
			};


			List<UserAccess> list = ReadList(sqlExpression, loginParam, passwordHashParam);

			if (list.Count != 1)
				throw new Exception();

			userAccessResult = list[0];


			return userAccessResult;

		}
		public bool LoginUserIsUnique(string login)
		{
			string sqlExpression = @"UserAccess_SelectByLogin";

			SqlParameter loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = login
			};

			List<UserAccess> list = ReadList(sqlExpression, loginParam);

			if (list.Count != 0)
				return false;

			return true;

		}
		public int? Create(UserAccess item)
		{
			string sqlExpression = @"UserAccess_Create";

			SqlParameter loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = item.Login
			};
			SqlParameter passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = item.PasswordHash
			};
			SqlParameter statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.Status
			};
			SqlParameter roleParam = new SqlParameter
			{
				ParameterName = "@Role",
				Value = item.Role
			};

			object resultCommand = ExecuteCommand(sqlExpression, loginParam, passwordHashParam, statusParam, roleParam);

			int? result = null;

			if (resultCommand != null)
				result = (int)resultCommand;


			return result;
		}
		public bool Delete(int id)
		{
			string sqlExpression = @"UserAccess_Delete";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdAccess",
				Value = id
			};

			//object result = ExecuteCommand(sqlExpression, idParam);
			ExecuteCommand(sqlExpression, idParam);

			return true;
		}
		public bool Update(UserAccess item)
		{
			string sqlExpression = @"UserAccess_Update";

			if (!item.IdEntity.HasValue)
				throw new ArgumentException($"Parameter{nameof(item.IdEntity)} must be not empty", nameof(item.IdEntity));

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			SqlParameter loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = item.Login
			};
			SqlParameter passwordHashParam = new SqlParameter
			{
				ParameterName = "@PasswordHash",
				Value = item.PasswordHash
			};
			SqlParameter statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.Status
			};
			SqlParameter roleParam = new SqlParameter
			{
				ParameterName = "@Role",
				Value = item.Role
			};

			//object resultCommand = ExecuteCommand(sqlExpression, idParam, loginParam, passwordHashParam, statusParam, roleParam);
			ExecuteCommand(sqlExpression, idParam, loginParam, passwordHashParam, statusParam, roleParam);


			return true;

		}

		public List<UserAccess> Find(Func<UserAccess, bool> predicate)
		{
			string sqlExpression = @"UserAccess_GetAll";

			List<UserAccess> listTemp = ReadList(sqlExpression);

			var listResult = listTemp.Where(predicate);

			return listResult.ToList();
		}

		public List<UserAccess> GetAll()
		{
			string sqlExpression = @"UserAccess_SelectAll";

			List<UserAccess> list = ReadList(sqlExpression);

			return list;
		}

		public UserAccess GetById(int id)
		{
			UserAccess userAccessResult;

			// название процедуры
			string sqlExpression = @"UserAccess_SelectById";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			List<UserAccess> list = ReadList(sqlExpression, idParam);

			if (list.Count != 1)
				return null;

			userAccessResult = list[0];


			return userAccessResult;
		}






		private List<UserAccess> ReadList(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			List<UserAccess> list = new List<UserAccess>();

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

				if (reader.HasRows) // если есть данные
				{
					while (reader.Read()) // построчно считываем данные
					{

						int readId = reader.GetInt32(0);
						string readLogin = reader.GetString(1);
						string readPassword = reader.GetString(2);
						string readStatus = reader.GetString(3);
						string readRole = reader.GetString(4);

						UserAccess userAccess = new UserAccess(readId, readLogin, readPassword, readStatus, readRole);

						list.Add(userAccess);

					}
				}


			}











			return list;
		}

		

	}
}
