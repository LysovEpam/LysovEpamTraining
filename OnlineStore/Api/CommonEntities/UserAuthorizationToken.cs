using System;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserAuthorizationToken : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static TimeSpan SessionKeyWork { get; } = new TimeSpan(1, 0, 0);
		public static int UserTokenMaxLength { get; } = 50;

		#endregion

		#region Свойства класса

		private string _sessionKey;
		private string _status;
		private int _userId;

		private AuthorizationStatus _authorizationStatus;
		private UserSystem _userSystem;


		public DateTime StartSession { get; set; }
		public DateTime FinishSession { get; set; }
		public string UserToken
		{
			get => _sessionKey;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(UserToken)} must not be empty", $"{nameof(UserToken)}");
				if (value.Length > UserTokenMaxLength)
					throw new ArgumentException($"The parameter {nameof(UserToken)} must be {UserTokenMaxLength} characters", $"{ nameof(UserToken) }");


				_sessionKey = value;
			}
		}
		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				_authorizationStatus = new AuthorizationStatus(value);
			}
		}
		public int UserId
		{
			get => _userId;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(UserId)} mast be more zero", $"{nameof(UserId)}");

				_userId = value;
			}
		}

		public AuthorizationStatus AuthorizationStatus
		{
			get => _authorizationStatus;
			set
			{
				_authorizationStatus = value ??
					throw new ArgumentException($"The parameter {nameof(AuthorizationStatus)} must not be empty",
						$"{nameof(AuthorizationStatus)}");

				_status = value.GetStatusName();
			}

		}
		public UserSystem UserSystem
		{
			get => _userSystem;
			set
			{
				_userSystem = value;
				if (value?.IdEntity != null)
					UserId = value.IdEntity.Value;
			}
		}

		#endregion

		#region Конструктор

		public UserAuthorizationToken(int idAuthorization, DateTime startSession, DateTime finishSession,
			string userToken, AuthorizationStatus authorizationStatus, UserSystem userSystem) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			AuthorizationStatus = authorizationStatus;
			UserSystem = userSystem;
		}
		public UserAuthorizationToken(int idAuthorization, DateTime startSession, DateTime finishSession,
			string userToken, string authorizationStatus, int userId) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;
			Status = authorizationStatus;
			UserId = userId;
		}
		public UserAuthorizationToken(DateTime startSession, DateTime finishSession,
			string userToken, AuthorizationStatus authorizationStatus, UserSystem userSystem)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			UserToken = userToken;

			AuthorizationStatus = authorizationStatus;
			UserSystem = userSystem;
		}

		#endregion
	}
}
