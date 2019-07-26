using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserSystemRepository : ExecuteCommandBase, IRepositoryUserSystem
	{
		#region Stored procedure names

		const string SpSelectByLoginPassword = @"UserSystem_SelectByLoginPassword";
		const string SpSelectByLogin = @"UserSystem_SelectByLogin";
		const string SpInsert = @"UserSystem_Insert";
		const string SpUpdate = @"UserSystem_Update";
		const string SpDelete = @"UserSystem_Delete";
		const string SpSelectById = @"UserSystem_SelectById";
		const string SpSelectAll = @"UserSystem_SelectAll";


		#endregion

		private readonly string _connectionString;
		private readonly UserAdmittanceRepository _userAdmittanceRepository;

		public UserSystemRepository(string connectionString, UserAdmittanceRepository userAdmittanceRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_userAdmittanceRepository = userAdmittanceRepository;
		}

		public UserSystem GetUserByLoginPasswordhash(string login, string passwordHash)
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


		public int? Insert(UserSystem item)
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
				ParameterName = "@UserAdmittanceId",
				Value = item.UserAdmittanceId
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
		public bool Update(UserSystem item)
		{
			if (!item.IdEntity.HasValue)
				throw new Exception($"Exception in {nameof(UserSystemRepository)}-{nameof(Update)}: id must be more 0");


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
				ParameterName = "@UserAdmittanceId",
				Value = item.UserAdmittanceId
			};

			var resultCommand = ExecuteCommand(SpUpdate, idParam, firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

			return resultCommand != null && (int)resultCommand == 1;

			
		}
		public bool Delete(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(UserSystemRepository)}-{nameof(Delete)}: id must be more 0");


			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			var resultCommand = ExecuteCommand(SpDelete, idParam);

			return resultCommand != null && (int)resultCommand == 1;
			
		}


		public UserSystem SelectById(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(UserSystemRepository)}-{nameof(SelectById)}: id must be more 0");

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			var user = ReadUser(SpSelectById, idParam);

			return user;
			

		}
		public List<UserSystem> SelectAll()
		{
			
			var users = ReadUsers(SpSelectAll);

			return users;
			
		}
		public List<UserSystem> Find(Func<UserSystem, bool> predicate)
		{
			
			var listTemp = ReadUsers(SpSelectAll);

			var users = listTemp.Where(predicate);

			return users.ToList();
			
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

						UserAdmittance userAdmittance = _userAdmittanceRepository.SelectById(readAccessId);

						UserSystem userSystem = new UserSystem(readId, readFirstName, readLastName, readEmail, readPhone, userAdmittance);

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

						UserAdmittance userAdmittance = _userAdmittanceRepository.SelectById(readAccessId);

						UserSystem userSystem = new UserSystem(readId, readFirstName, readLastName, readEmail, readPhone, userAdmittance);

						return userSystem;

					}
				}


			}

			return null;
		}

		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}

		public UserSystem GetUserByLogin(string login)
		{
			var loginParam = new SqlParameter
			{
				ParameterName = "@Login",
				Value = login
			};
			
			var user = ReadUser(SpSelectByLogin, loginParam);

			return user;
		}
	}
}
