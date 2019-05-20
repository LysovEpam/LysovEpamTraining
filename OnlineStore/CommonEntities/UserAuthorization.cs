using System;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserAuthorization : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static TimeSpan SessionKeyWord { get; } = new TimeSpan(1, 0, 0);
		public static int SessionKeyMaxLength { get; } = 50;

		#endregion

		#region Свойства класса

		private string _sessionKey;
		private string _status;
		private int _userId;

		private AuthorizationStatus _authorizationStatus;
		private User _user;


		public DateTime StartSession { get; set; }
		public DateTime FinishSession { get; set; }
		public string SessionKey
		{
			get => _sessionKey;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(SessionKey)} must not be empty", $"{nameof(SessionKey)}");
				if (value.Length > SessionKeyMaxLength)
					throw new ArgumentException($"The parameter {nameof(SessionKey)} must be {SessionKeyMaxLength} characters", $"{ nameof(SessionKey) }");


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
		public User User
		{
			get => _user;
			set
			{
				_user = value;
				if (value?.IdEntity != null)
					UserId = value.IdEntity.Value;
			}
		}

		#endregion

		#region Конструктор

		public UserAuthorization(int idAuthorization, DateTime startSession, DateTime finishSession,
			string sessionKey, AuthorizationStatus authorizationStatus, User user) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			SessionKey = sessionKey;
			AuthorizationStatus = authorizationStatus;
			User = user;
		}
		public UserAuthorization(int idAuthorization, DateTime startSession, DateTime finishSession,
			string sessionKey, string authorizationStatus, int userId) : base(idAuthorization)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			SessionKey = sessionKey;
			Status = authorizationStatus;
			UserId = userId;
		}
		public UserAuthorization(DateTime startSession, DateTime finishSession,
			string sessionKey, AuthorizationStatus authorizationStatus, User user)
		{
			StartSession = startSession;
			FinishSession = finishSession;
			SessionKey = sessionKey;

			AuthorizationStatus = authorizationStatus;
			User = user;
		}

		#endregion
	}
}
