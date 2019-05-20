using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts.Repositories;


namespace DAL.OnlineStore.Repositories
{
	public class UserAuthorizationRepository : ExecuteCommandBase, IRepositoryUserAuthorization
	{

		private readonly string _connectionString;

		public UserAuthorizationRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}


		public bool CancelSessionKeys(int idUser)
		{
			
			string sqlExpression = @"UserAuthorization_CancelSessionKey";
			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = idUser
			};
			SqlParameter oldStatusParam = new SqlParameter
			{
				ParameterName = "@OldStatus",
				Value = AuthorizationStatus.GetStatusName(AuthorizationStatus.AuthorizationStatusEnum.Active)
			};
			SqlParameter finishKeyParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = DateTime.Now
			};
			SqlParameter newStatusParam = new SqlParameter
			{
				ParameterName = "@NewStatus",
				Value = AuthorizationStatus.GetStatusName(AuthorizationStatus.AuthorizationStatusEnum.BlockNewAuthorization)
			};

			//object resultCommand = ExecuteCommand(sqlExpression, idParam, oldStatusParam, finishKeyParam, newStatusParam);
			ExecuteCommand(sqlExpression, idParam, oldStatusParam, finishKeyParam, newStatusParam);
			
			return true;

		}
		public bool CancelSessionKeys(User user)
		{

			if(!user.IdEntity.HasValue)
				throw new ArgumentException($"Parameter {nameof(user.IdEntity)} must be not empty", nameof(user.IdEntity));

			string sqlExpression = @"UserAuthorization_CancelSessionKey";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = user.IdEntity.Value
			};
			SqlParameter oldStatusParam = new SqlParameter
			{
				ParameterName = "@OldStatus",
				Value = AuthorizationStatus.GetStatusName(AuthorizationStatus.AuthorizationStatusEnum.Active)
			};
			SqlParameter finishKeyParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = DateTime.Now
			};
			SqlParameter newStatusParam = new SqlParameter
			{
				ParameterName = "@NewStatus",
				Value = AuthorizationStatus.GetStatusName(AuthorizationStatus.AuthorizationStatusEnum.BlockNewAuthorization)
			};

			//object resultCommand = ExecuteCommand(sqlExpression, idParam, oldStatusParam, finishKeyParam, newStatusParam);
			ExecuteCommand(sqlExpression, idParam, oldStatusParam, finishKeyParam, newStatusParam);

			return true;
		}

		public int? Create(UserAuthorization item)
		{
			string sqlExpression = @"UserAuthorization_Insert";

			SqlParameter startParam = new SqlParameter
			{
				ParameterName = "@StartSession",
				Value = item.StartSession
			};
			SqlParameter finishParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = item.FinishSession
			};
			SqlParameter keyParam = new SqlParameter
			{
				ParameterName = "@SessionKey",
				Value = item.SessionKey
			};
			SqlParameter statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.Status
			};
			SqlParameter userIdParam = new SqlParameter
			{
				ParameterName = "@UserId",
				Value = item.UserId
			};

			object resultCommand = ExecuteCommand(sqlExpression, startParam, finishParam, keyParam, statusParam, userIdParam);

			int? result = null;

			if (resultCommand != null)
				result = (int)resultCommand;


			return result;

		}

		public bool Update(UserAuthorization item)
		{
			if (!item.IdEntity.HasValue)
				throw new ArgumentException($"Parameter {nameof(item.IdEntity)} must be not empty", nameof(item.IdEntity));

			
			string sqlExpression = @"UserAuthorization_Update";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			SqlParameter startParam = new SqlParameter
			{
				ParameterName = "@StartSession",
				Value = item.StartSession
			};
			SqlParameter finishParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = item.FinishSession
			};
			SqlParameter keyParam = new SqlParameter
			{
				ParameterName = "@SessionKey",
				Value = item.SessionKey
			};
			SqlParameter statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.Status
			};
			SqlParameter userIdParam = new SqlParameter
			{
				ParameterName = "@UserId",
				Value = item.UserId
			};

			//object resultCommand = ExecuteCommand(sqlExpression, idParam, startParam, finishParam, keyParam, statusParam, userIdParam);
			ExecuteCommand(sqlExpression, idParam, startParam, finishParam, keyParam, statusParam, userIdParam);


			return true;


		}
		public bool Delete(int id)
		{
			
			string sqlExpression = @"UserAuthorization_Delete";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdAccess",
				Value = id
			};

			//object result = ExecuteCommand(sqlExpression, idParam);
			ExecuteCommand(sqlExpression, idParam);

			return true;
		}

		public UserAuthorization GetById(int id)
		{
			UserAuthorization userAuthorizationResult;

			string sqlExpression = @"UserAuthorization_SelectById";

			SqlParameter idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			List<UserAuthorization> list = ReadList(sqlExpression, idParam);

			if (list.Count != 1)
				return null;

			userAuthorizationResult = list[0];


			return userAuthorizationResult;
		}
		public List<UserAuthorization> GetAll()
		{
			string sqlExpression = @"UserAuthorization_SelectAll";

			List<UserAuthorization> list = ReadList(sqlExpression);

			return list;
		}
		public List<UserAuthorization> Find(Func<UserAuthorization, bool> predicate)
		{
			string sqlExpression = @"UserAuthorization_SelectAll";

			List<UserAuthorization> listTemp = ReadList(sqlExpression);

			var listResult = listTemp.Where(predicate);

			return listResult.ToList();
		}

		


		private List<UserAuthorization> ReadList(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			List<UserAuthorization> list = new List<UserAuthorization>();

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
						DateTime readDateStart = reader.GetDateTime(1);
						DateTime readDateFinish = reader.GetDateTime(2);
						string readKey = reader.GetString(3);
						string readStatus = reader.GetString(4);
						int readUserId = reader.GetInt32(5);

						UserAuthorization userAuthorization = new UserAuthorization(readId, readDateStart, readDateFinish, readKey, readStatus, readUserId);

						list.Add(userAuthorization);

					}
				}


			}











			return list;
		}


	}
}
