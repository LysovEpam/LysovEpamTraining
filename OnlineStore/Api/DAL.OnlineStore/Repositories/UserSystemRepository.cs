using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserSystemRepository : ExecuteCommandBase, IRepositoryUserSystem, ILogRepository
	{
		#region Stored procedure names

		const string SpSelectByLoginPassword = @"UserSystem_SelectByLoginPassword";
		const string SpInsert = @"UserSystem_Insert";
		const string SpUpdate = @"UserSystem_Update";
		const string SpDelete = @"UserSystem_Delete";
		const string SpSelectById = @"UserSystem_SelectById";
		const string SpSelectAll = @"UserSystem_SelectAll";


		#endregion

		private readonly string _connectionString;
		private readonly UserAccessRepository _userAccessRepository;

		public UserSystemRepository(string connectionString, UserAccessRepository userAccessRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_userAccessRepository = userAccessRepository;
		}

		public UserSystem GetUserByLoginPasswordhash(string login, string passwordHash)
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

				var user = ReadUser(SpSelectByLoginPassword, loginParam, passwordHashParam);

				return user;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(GetUserByLoginPasswordhash)}",
					$"Ошибка при получении сущности {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}


		public int? Insert(UserSystem item)
		{
			try
			{
				var firstNameParam = new SqlParameter
				{
					ParameterName = "@FirstName",
					Value = item.FirsName
				};
				var lastNameParam = new SqlParameter
				{
					ParameterName = "@LastName",
					Value = item.LastName
				};
				var emailParam = new SqlParameter
				{
					ParameterName = "@Email",
					Value = item.Email
				};
				var phoneParam = new SqlParameter
				{
					ParameterName = "@Phone",
					Value = item.Phone
				};
				var userAccessIdParam = new SqlParameter
				{
					ParameterName = "@UserAccessId",
					Value = item.UserAccessId
				};

				var resultCommand = ExecuteCommand(SpInsert, firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

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
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Insert)}",
					$"Ошибка при создании сущности {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public bool Update(UserSystem item)
		{

			if (!item.IdEntity.HasValue)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(UserSystem)}",
					"Id сущности не может быть пустым");

				return false;
			}


			try
			{

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = item.IdEntity.Value
				};
				var firstNameParam = new SqlParameter
				{
					ParameterName = "@FirstName",
					Value = item.FirsName
				};
				var lastNameParam = new SqlParameter
				{
					ParameterName = "@LastName",
					Value = item.LastName
				};
				var emailParam = new SqlParameter
				{
					ParameterName = "@Email",
					Value = item.Email
				};
				var phoneParam = new SqlParameter
				{
					ParameterName = "@Phone",
					Value = item.Phone
				};
				var userAccessIdParam = new SqlParameter
				{
					ParameterName = "@UserAccessId",
					Value = item.UserAccessId
				};
				
				var resultCommand = ExecuteCommand(SpUpdate, idParam, firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

				return resultCommand != null && (int)resultCommand == 1;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Insert)}",
					$"Ошибка при создании сущности {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return false;
			}
		}
		public bool Delete(int id)
		{
			if (id < 1)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(UserSystem)}",
					"Id не может быть меньше 1");

				return false;
			}
			
			try
			{

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				var resultCommand = ExecuteCommand(SpDelete, idParam);

				return resultCommand != null && (int)resultCommand == 1;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(UserSystem)}",
					$"Текст инсключения: {e.Message}");

				return false;
			}
		}


		public UserSystem SelectById(int id)
		{
			try
			{
				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				var user = ReadUser(SpSelectById, idParam);

				return user;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(SelectById)}",
					$"Ошибка при получении сущности {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return null;
			}

		}
		public List<UserSystem> SelectAll()
		{
			try
			{
				var users = ReadUsers(SpSelectAll);

				return users;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(SelectAll)}",
					$"Ошибка при получении списка сущностей {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<UserSystem> Find(Func<UserSystem, bool> predicate)
		{
			try
			{
				var listTemp = ReadUsers(SpSelectAll);

				var users = listTemp.Where(predicate);

				return users.ToList();
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserSystemRepository)} - {nameof(Find)}",
					$"Ошибка при поиске в списке сущностей {nameof(UserSystem)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}



		private List<UserSystem> ReadUsers(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			List<UserSystem> users = new List<UserSystem>();

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
						string readFirstName = reader.GetString(1);
						string readLastName = reader.GetString(2);
						string readEmail = reader.GetString(3);
						string readPhone = reader.GetString(4);
						int readAccessId = reader.GetInt32(5);

						UserAccess userAccess = _userAccessRepository.SelectById(readAccessId);

						UserSystem userSystem = new UserSystem(readId, readFirstName, readLastName, readEmail, readPhone, userAccess);

						users.Add(userSystem);

					}
				}


			}


			return users;
		}
		private UserSystem ReadUser(string storedProcedureName, params SqlParameter[] sqlParameters)
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
						string readFirstName = reader.GetString(1);
						string readLastName = reader.GetString(2);
						string readEmail = reader.GetString(3);
						string readPhone = reader.GetString(4);
						int readAccessId = reader.GetInt32(5);

						UserAccess userAccess = _userAccessRepository.SelectById(readAccessId);

						UserSystem userSystem = new UserSystem(readId, readFirstName, readLastName, readEmail, readPhone, userAccess);

						return userSystem;

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
