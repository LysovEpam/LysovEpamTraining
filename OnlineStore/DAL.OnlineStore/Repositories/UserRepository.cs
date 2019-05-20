using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserRepository : ExecuteCommandBase, IRepositoryUser
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		public User GetUserByLoginPasswordhash(string login, string passwordHash)
		{
			User userResult;

			string sqlExpression = @"UserSystem_SelectByLoginPassword";

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


			List<User> list = ReadList(sqlExpression, loginParam, passwordHashParam);

			if (list.Count != 1)
				throw new Exception();

			userResult = list[0];
			
			return userResult;
		}


		public int? Create(User item)
		{
			string sqlExpression = @"UserSystem_Create";

			SqlParameter firstNameParam = new SqlParameter
			{
				ParameterName = "@FirstName",
				Value = item.FirsName
			};
			SqlParameter lastNameParam = new SqlParameter
			{
				ParameterName = "@LastName",
				Value = item.LastName
			};
			SqlParameter emailParam = new SqlParameter
			{
				ParameterName = "@Email",
				Value = item.Email
			};
			SqlParameter phoneParam = new SqlParameter
			{
				ParameterName = "@Phone",
				Value = item.Phone
			};
			SqlParameter userAccessIdParam = new SqlParameter
			{
				ParameterName = "@UserAccess_Id",
				Value = item.UserAccessId
			};

			object resultCommand = ExecuteCommand(sqlExpression, firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

			int? result = null;

			if (resultCommand != null)
				result = (int)resultCommand;


			return result;
		}
		public bool Update(User item)
		{
			if (!item.IdEntity.HasValue)
				throw new ArgumentException($"Parameter {nameof(item.IdEntity)} must be not empty", nameof(item.IdEntity));


			string sqlExpression = @"UserSystem_Update";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};

			SqlParameter firstNameParam = new SqlParameter
			{
				ParameterName = "@FirstName",
				Value = item.FirsName
			};
			SqlParameter lastNameParam = new SqlParameter
			{
				ParameterName = "@LastName",
				Value = item.LastName
			};
			SqlParameter emailParam = new SqlParameter
			{
				ParameterName = "@Email",
				Value = item.Email
			};
			SqlParameter phoneParam = new SqlParameter
			{
				ParameterName = "@Phone",
				Value = item.Phone
			};
			SqlParameter userAccessIdParam = new SqlParameter
			{
				ParameterName = "@UserAccess_Id",
				Value = item.UserAccessId
			};

			//object resultCommand = ExecuteCommand(sqlExpression, firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

			ExecuteCommand(sqlExpression, idParam,firstNameParam, lastNameParam, emailParam, phoneParam, userAccessIdParam);

			return true;
		}


		public bool Delete(int id)
		{
			string sqlExpression = @"UserSystem_Delete";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdAccess",
				Value = id
			};

			//object result = ExecuteCommand(sqlExpression, idParam);
			ExecuteCommand(sqlExpression, idParam);

			return true;
		}
		public User GetById(int id)
		{
			User userResult;

			string sqlExpression = @"UserSystem_SelectById";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			List<User> list = ReadList(sqlExpression, idParam);

			if (list.Count != 1)
				return null;

			userResult = list[0];

			return userResult;

		}
		public List<User> GetAll()
		{
			string sqlExpression = @"UserSystem_SelectAll";

			List<User> list = ReadList(sqlExpression);

			return list;
		}

		public List<User> Find(Func<User, bool> predicate)
		{
			string sqlExpression = @"UserSystem_SelectAll";

			List<User> listTemp = ReadList(sqlExpression);

			var listResult = listTemp.Where(predicate);

			return listResult.ToList();
		}

		

		


		private List<User> ReadList(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			List<User> list = new List<User>();

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

						User userAccess = new User(readId, readFirstName, readLastName, readEmail, readPhone, readAccessId);

						list.Add(userAccess);

					}
				}


			}











			return list;
		}



	}
}
