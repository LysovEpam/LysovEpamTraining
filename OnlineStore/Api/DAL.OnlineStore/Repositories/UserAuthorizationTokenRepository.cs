using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts.Repositories;


namespace DAL.OnlineStore.Repositories
{
	public class UserAuthorizationTokenRepository : ExecuteCommandBase, IRepositoryUserAuthorizationToken
	{

		#region Stored procedure names

		const string SpCancelSessionKey = @"UserAuthorizationToken_CancelSessionKey";
		const string SpSelectByToken = @"UserAuthorizationToken_SelectByToken";
		const string SpInsert = @"UserAuthorizationToken_Insert";
		const string SpUpdate = @"UserAuthorizationToken_Update";
		const string SpDelete = @"UserAuthorizationToken_Delete";
		const string SpSelectById = @"UserAuthorizationToken_SelectById";
		const string SpSelectAll = @"UserAuthorizationToken_SelectById";

		#endregion

		private readonly string _connectionString;
		private readonly UserSystemRepository _repositorySystemRepository;

		public UserAuthorizationTokenRepository(string connectionString, UserSystemRepository userSystemRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_repositorySystemRepository = userSystemRepository;
		}


		public bool CancelSessionKeys(int idUser, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
		{
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = idUser
			};
			var oldStatusParam = new SqlParameter
			{
				ParameterName = "@OldStatus",
				Value = oldStatus.GetStatusName()
			};
			var finishKeyParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = dateBlock
			};
			var newStatusParam = new SqlParameter
			{
				ParameterName = "@NewStatus",
				Value = newStatus.GetStatusName()
			};

			ExecuteCommand(SpCancelSessionKey, idParam, oldStatusParam, finishKeyParam, newStatusParam);

			return true;

		}
		public bool CancelSessionKeys(UserSystem userSystem, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
		{
			if (!userSystem.IdEntity.HasValue)
				throw new ArgumentException($"Parameter {nameof(userSystem.IdEntity)} must be not empty", nameof(userSystem.IdEntity));

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = userSystem.IdEntity.Value
			};
			var oldStatusParam = new SqlParameter
			{
				ParameterName = "@OldStatus",
				Value = oldStatus.GetStatusName()
			};
			var finishKeyParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = dateBlock
			};
			var newStatusParam = new SqlParameter
			{
				ParameterName = "@NewStatus",
				Value = newStatus.GetStatusName()
			};

			ExecuteCommand(SpCancelSessionKey, idParam, oldStatusParam, finishKeyParam, newStatusParam);

			return true;
		}

		public UserAuthorizationToken GetByToken(string token)
		{
			var tokenParam = new SqlParameter
			{
				ParameterName = "@UserToken",
				Value = token
			};

			var result = ReadUserAuthorization(SpSelectByToken, tokenParam);

			return result;


		}

		public int? Insert(UserAuthorizationToken item)
		{
			var startParam = new SqlParameter
			{
				ParameterName = "@StartSession",
				Value = item.StartSession
			};
			var finishParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = item.FinishSession
			};
			var tokenParam = new SqlParameter
			{
				ParameterName = "@UserToken",
				Value = item.UserToken
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.AuthorizationStatus.GetStatusName()
			};
			var userIdParam = new SqlParameter
			{
				ParameterName = "@UserId",
				Value = item.UserId
			};

			object resultCommand =
				ExecuteCommand(SpInsert, startParam, finishParam, tokenParam, statusParam, userIdParam);

			int? result = null;

			if (resultCommand != null)
			{
				decimal lastId = (decimal)resultCommand;
				result = Decimal.ToInt32(lastId);
			}

			return result;
		}
		public bool Update(UserAuthorizationToken item)
		{
			if (!item.IdEntity.HasValue)
				throw new ArgumentException($"Parameter {nameof(item.IdEntity)} must be not empty", nameof(item.IdEntity));

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			var startParam = new SqlParameter
			{
				ParameterName = "@StartSession",
				Value = item.StartSession
			};
			var finishParam = new SqlParameter
			{
				ParameterName = "@FinishSession",
				Value = item.FinishSession
			};
			var tokenParam = new SqlParameter
			{
				ParameterName = "@UserToken",
				Value = item.UserToken
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.AuthorizationStatus.GetStatusName()
			};
			var userIdParam = new SqlParameter
			{
				ParameterName = "@UserId",
				Value = item.UserId
			};

			var resultCommand = ExecuteCommand(SpUpdate, idParam, startParam, finishParam, tokenParam, statusParam, userIdParam);

			return resultCommand != null && (int)resultCommand == 1;
		}
		public bool Delete(int id)
		{
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			//object result = ExecuteCommand(sqlExpression, idParam);
			var resultCommand = ExecuteCommand(SpDelete, idParam);

			return resultCommand != null && (int)resultCommand == 1;
		}

		public UserAuthorizationToken SelectById(int id)
		{
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			var userAuthorization = ReadUserAuthorization(SpSelectById, idParam);

			return userAuthorization;
		}
		public List<UserAuthorizationToken> SelectAll()
		{
			List<UserAuthorizationToken> list = ReadUserAuthorizations(SpSelectAll);

			return list;
		}
		public List<UserAuthorizationToken> Find(Func<UserAuthorizationToken, bool> predicate)
		{
			var listTemp = SelectAll();

			var listResult = listTemp.Where(predicate);

			return listResult.ToList();
		}



		private List<UserAuthorizationToken> ReadUserAuthorizations(string storedProcedureName, params SqlParameter[] sqlParameters)
		{
			var userAuthorizations = new List<UserAuthorizationToken>();

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
						string readToken = reader.GetString(3);
						string readStatus = reader.GetString(4);
						int readUserId = reader.GetInt32(5);

						UserSystem user = _repositorySystemRepository.SelectById(readUserId);

						UserAuthorizationToken userAuthorization = new UserAuthorizationToken(readId, readDateStart,
							readDateFinish, readToken, readStatus, user);

						userAuthorizations.Add(userAuthorization);

					}
				}


			}


			return userAuthorizations;
		}
		private UserAuthorizationToken ReadUserAuthorization(string storedProcedureName, params SqlParameter[] sqlParameters)
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
						DateTime readDateStart = reader.GetDateTime(1);
						DateTime readDateFinish = reader.GetDateTime(2);
						string readKey = reader.GetString(3);
						string readStatus = reader.GetString(4);
						int readUserId = reader.GetInt32(5);

						UserSystem user = _repositorySystemRepository.SelectById(readUserId);


						UserAuthorizationToken userAuthorizationToken = new UserAuthorizationToken(readId, readDateStart, readDateFinish, readKey, readStatus, user);

						return userAuthorizationToken;

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
