using System;
using System.Globalization;
using BLContracts;
using BLContracts.SystemBl;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.SystemBlModel
{
	public class AuthorizationBlModel : IAuthorizationBlModel
	{

		private readonly IDbContext _dbContext;
		private readonly IPasswordHash _passwordHash;
		public AuthorizationBlModel(IDbContext dbContext, IPasswordHash passwordHash)
		{
			_dbContext = dbContext;
			_passwordHash = passwordHash;
		}


		public UserStatus CheckUserStatus(string login, string password)
		{
			string passwordHash = _passwordHash.GeneratePasswordHash(login, password);

			UserAccess userAccess = _dbContext.UserAccesses.GetUserAccess(login, passwordHash);

			if (userAccess?.IdEntity == null)
				return null;

			return userAccess.UserStatus;
		}

		public (UserStatus, string) GetAuthorizationToken(string login, string password)
		{
			UserStatus userStatus = CheckUserStatus(login, password);

			if (userStatus?.Status != UserStatus.StatusEnum.Active)
				return (userStatus, null);


			string passwordHash = _passwordHash.GeneratePasswordHash(login, password);

			User user = _dbContext.Users.GetUserByLoginPasswordhash(login, passwordHash);

			if (user == null)
				return (userStatus, null);


			string sessionKey = GenerateSessionKey(login, password);

			SaveSessionKey(user, sessionKey);


			return (userStatus, sessionKey);
		}

		private string GenerateSessionKey(string login, string password)
		{
			int loginHashCode = login.GetHashCode();
			int passwordHashCode = _passwordHash.GeneratePasswordHash(login, password).GetHashCode();
			int dateHashCode = DateTime.Now.ToString(CultureInfo.InvariantCulture).GetHashCode();


			string loginKey = Math.Abs(loginHashCode).ToString();
			string passwordKey = Math.Abs(passwordHashCode).ToString();
			string dateKey = Math.Abs(dateHashCode).ToString();
			string tickKey = DateTime.Now.Ticks.ToString();

			string result = loginKey + passwordKey + dateKey + tickKey;

			return result;
		}

		private void SaveSessionKey(User user, string sessionKey)
		{
			DateTime dateStart = DateTime.Now;
			DateTime dateFinish = DateTime.Now + UserAuthorization.SessionKeyWord;
			AuthorizationStatus status = new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active);

			UserAuthorization newAuthorizationUser = new UserAuthorization(dateStart, dateFinish, sessionKey, status, user);

			_dbContext.UserAuthorizations.Create(newAuthorizationUser);
		}


	}
}
