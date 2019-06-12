using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserAccessRepository : ExecuteCommandBase, IRepositoryUserAccess, ILogRepository
	{
		#region Stored procedure names

		const string SpSelectByLoginPassword = @"UserAccess_SelectByLoginPassword";
		const string SpSelectByLogin = @"UserAccess_SelectByLogin";
		const string SpInsert = @"UserAccess_Insert";
		const string SpDelete = @"UserAccess_Delete";
		const string SpUpdate = @"UserAccess_Update";
		const string SpSelectById = @"UserAccess_SelectById";
		const string SpSelectAll = @"UserAccess_SelectAll";

		#endregion

		private readonly string _connectionString;

		public UserAccessRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		public UserAccess GetUserAccess(string login, string passwordHash)
		{
			try
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(GetUserAccess)}",
					$"Ошибка при чтении сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public bool LoginUserIsUnique(string login)
		{
			try
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(LoginUserIsUnique)}",
					$"Ошибка при проверке сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return false;
			}
		}


		public int? Insert(UserAccess item)
		{
			try
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
					Value = item.Status
				};
				var roleParam = new SqlParameter
				{
					ParameterName = "@Role",
					Value = item.Role
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(Insert)}",
					$"Ошибка при создании новой сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public bool Update(UserAccess item)
		{
			try
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
					Value = item.Status
				};
				var roleParam = new SqlParameter
				{
					ParameterName = "@Role",
					Value = item.Role
				};

				var resultCommand = ExecuteCommand(SpUpdate, idParam, loginParam, passwordHashParam, statusParam, roleParam);


				return resultCommand != null && (int)resultCommand == 1;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return false;
			}

		}
		public bool Delete(int id)
		{
			try
			{

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				object resultCommand = ExecuteCommand(SpDelete, idParam);

				return resultCommand != null && (int)resultCommand == 1;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return false;
			}
		}


		public UserAccess SelectById(int id)
		{
			try
			{

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				var userAccess = ReadUserAccess(SpSelectById, idParam);

				return userAccess;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(SelectById)}",
					$"Ошибка при чтении сущности {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<UserAccess> SelectAll()
		{
			try
			{
				var userAccesses = ReadUserAccesses(SpSelectAll);

				return userAccesses;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(SelectAll)}",
					$"Ошибка при чтении списка сущностей {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<UserAccess> Find(Func<UserAccess, bool> predicate)
		{
			try
			{
				var listTemp = ReadUserAccesses(SpSelectAll);

				var userAccesses = listTemp.Where(predicate);

				return userAccesses.ToList();
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserAccessRepository)} - {nameof(Find)}",
					$"Ошибка при поиске в списке сущностей {nameof(UserAccess)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}


		private List<UserAccess> ReadUserAccesses(string storedProcedureName, params SqlParameter[] sqlParameters)
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

				if (reader.HasRows)
				{
					while (reader.Read())
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
		private UserAccess ReadUserAccess(string storedProcedureName, params SqlParameter[] sqlParameters)
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

						var userAccess = new UserAccess(readId, readLogin, readPassword, readStatus, readRole);

						return userAccess;
					}
				}
			}

			return null;


		}

		public event RepositoryEvent RepositoryEvent;

		private void DoRepositoryEvent(string location, string caption, string description)
		{
			RepositoryEvent?.Invoke(location, caption, description);
		}

	}
}
