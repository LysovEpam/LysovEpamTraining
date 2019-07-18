using System;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserAuthorizationToken : BaseDbEntity
	{
		#region Свойства класса

		public DateTime StartSession { get; set; }
		public DateTime FinishSession { get; set; }
		public string UserToken { get; set; }
		//public string Status { get; set; }
		public int UserId { get; set; }

		public AuthorizationStatus AuthorizationStatus { get; set; }
		public UserSystem UserSystem { get; set; }

		#endregion

		#region Конструктор

		public UserAuthorizationToken()
		{
			
		}
		public UserAuthorizationToken(int idAuthorization, DateTime startSession, DateTime finishSession,
			string userToken, AuthorizationStatus authorizationStatus, UserSystem userSystem) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			//Status = authorizationStatus.GetStatusName();
			//if (userSystem.IdEntity != null)
			//	UserId = userSystem.IdEntity.Value;

			AuthorizationStatus = authorizationStatus;
			UserSystem = userSystem;
		}
		public UserAuthorizationToken(int idAuthorization, DateTime startSession, DateTime finishSession,
			string userToken, string authorizationStatus, int userId) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			//Status = authorizationStatus;
			UserId = userId;

			AuthorizationStatus = new AuthorizationStatus(authorizationStatus);
			UserSystem = null;
		}
		public UserAuthorizationToken(DateTime startSession, DateTime finishSession,
			string userToken, AuthorizationStatus authorizationStatus, UserSystem userSystem)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			//Status = authorizationStatus.GetStatusName();
			//if (userSystem.IdEntity != null)
			//	UserId = userSystem.IdEntity.Value;

			AuthorizationStatus = authorizationStatus;
			UserSystem = userSystem;
		}
		public UserAuthorizationToken(DateTime startSession, DateTime finishSession,
			string userToken, AuthorizationStatus authorizationStatus, int userId)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			//Status = authorizationStatus.GetStatusName();
			UserId = userId;

			AuthorizationStatus = authorizationStatus;
			UserSystem = null;
		}

		public UserAuthorizationToken(int idAuthorization, DateTime startSession, DateTime finishSession,
			string userToken, string authorizationStatus, UserSystem userSystem) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			//Status = authorizationStatus;
			if (userSystem.IdEntity != null)
				UserId = userSystem.IdEntity.Value;

			AuthorizationStatus = new AuthorizationStatus(authorizationStatus);
			UserSystem = userSystem;
		}

		#endregion
	}
}
