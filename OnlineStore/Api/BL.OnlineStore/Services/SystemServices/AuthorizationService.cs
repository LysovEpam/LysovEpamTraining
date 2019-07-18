using System;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.SystemService;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services.SystemServices
{
	public class AuthorizationService : IAuthorizationService
	{

		private readonly IDbContext _dbContext;
		private readonly IPasswordHash _passwordHash;
		private readonly int _userTokenLength;
		private readonly TimeSpan _timeKeyWork;

		public AuthorizationService(IDbContext dbContext, IPasswordHash passwordHash)
		{
			_dbContext = dbContext;
			_passwordHash = passwordHash;

			_userTokenLength = 50;
			_timeKeyWork = new TimeSpan(24, 0, 0);
		}


		public ServiceResult CheckAuthorization(AuthorizationRequest authorizationData)
		{



			string hash = _passwordHash.GeneratePasswordHash(authorizationData.Login, authorizationData.Password);
			var userAdmittance = _dbContext.UserAdmittances.GetUserAdmittance(authorizationData.Login, hash);

			ServiceResult result = null;

			if (userAdmittance == null)
			{
				result = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"User with this login and password not found");
			}
			else if (userAdmittance.UserStatus.Status == UserStatus.StatusEnum.Active)
			{
				result = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			}
			else if (userAdmittance.UserStatus.Status == UserStatus.StatusEnum.Block)
			{
				result = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"The user is blocked");
			}
			else if (userAdmittance.UserStatus.Status == UserStatus.StatusEnum.Delete)
			{
				result = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"The user deleted");
			}

			return result;
		}

		public (UserRole userRole, string sessionToken, DateTime authorizationFinish) AuthorizationUser(AuthorizationRequest authorizationData)
		{
			string hash = _passwordHash.GeneratePasswordHash(authorizationData.Login, authorizationData.Password);
			var userSystem = _dbContext.UsersSystem.GetUserByLoginPasswordhash(authorizationData.Login, hash);

			var sessionToken = GenerateSessionToken(authorizationData.Login);

			UserAuthorizationToken userToken = new UserAuthorizationToken(DateTime.Now, DateTime.Now + _timeKeyWork,
				sessionToken, new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active),
				userSystem.IdEntity.Value);

			var cancelResult = CancelOldSessionToken(userSystem.IdEntity.Value);

			if (!cancelResult)
				return (null, null, DateTime.Now);

			var tokenInserResult = _dbContext.UserAuthorizationsToken.Insert(userToken);

			if (!tokenInserResult.HasValue)
				return (null, null, DateTime.Now);

			return (userSystem.UserAdmittance.UserRole, sessionToken, DateTime.Now + _timeKeyWork);

		}



		private bool CancelOldSessionToken(int idUser)
		{
			AuthorizationStatus oldStatus = new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active);
			AuthorizationStatus newStatus = new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.BlockNewAuthorization);

			var result = _dbContext.UserAuthorizationsToken.CancelSessionKeys(idUser, oldStatus, DateTime.Now, newStatus);

			return result;
		}



		private string GenerateSessionToken(string login)
		{
			int random = new Random().Next(10000000, 99999999);
			var ticks = DateTime.Now.Ticks;
			string numb = $"{ticks}{random}";

			string loginHash = _passwordHash.GeneratePasswordHash(login, random.ToString());


			string result = "";

			for (int i = 0; result.Length < _userTokenLength; i++)
			{
				result += loginHash[i];
				result += numb[i];
			}

			return result;
		}



		//private UserSystem GetUserSystem(string login, string passwrod)
		//{
		//	string hash = _passwordHash.GeneratePasswordHash(login, passwrod);

		//	var userSystem = _dbContext.UsersSystem.GetUserByLoginPasswordhash(login, hash);

		//	return userSystem;
		//}


		//=====================================================================================================================



		//public UserStatus GetUserStatus(string login, string password)
		//{
		//	string passwordHash = _passwordHash.GeneratePasswordHash(login, password);

		//	UserAccess userAccess = _dbContext.UserAccesses.GetUserAccess(login, passwordHash);

		//	if (userAccess?.IdEntity == null)
		//		return null;

		//	return userAccess.UserStatus;
		//}

		//public (UserStatus, string) GetAuthorizationToken(string login, string password)
		//{
		//	UserStatus userStatus = GetUserStatus(login, password);

		//	if (userStatus?.Status != UserStatus.StatusEnum.Active)
		//		return (userStatus, null);


		//	string passwordHash = _passwordHash.GeneratePasswordHash(login, password);

		//	User user = _dbContext.Users.GetUserByLoginPasswordhash(login, passwordHash);

		//	if (user == null)
		//		return (userStatus, null);


		//	string sessionKey = GenerateSessionKey(login, password);

		//	bool saveResult = SaveSessionKey(user, sessionKey);

		//	if(!saveResult)
		//		return (userStatus, null);

		//	return (userStatus, sessionKey);
		//}



		//private string GenerateSessionKey(string login, string password)
		//{
		//	int loginHashCode = login.GetHashCode();
		//	int passwordHashCode = _passwordHash.GeneratePasswordHash(login, password).GetHashCode();
		//	int dateHashCode = DateTime.Now.ToString(CultureInfo.InvariantCulture).GetHashCode();


		//	string loginKey = Math.Abs(loginHashCode).ToString();
		//	string passwordKey = Math.Abs(passwordHashCode).ToString();
		//	string dateKey = Math.Abs(dateHashCode).ToString();
		//	string tickKey = DateTime.Now.Ticks.ToString();

		//	string result = loginKey + passwordKey + dateKey + tickKey;

		//	return result;
		//}
		//private bool SaveSessionKey(User user, string sessionKey)
		//{
		//	#region Создать новый ключ

		//	DateTime dateStart = DateTime.Now;
		//	DateTime dateFinish = DateTime.Now + UserAuthorizationToken.SessionKeyWork;
		//	AuthorizationStatus status = 
		//		new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active);

		//	UserAuthorizationToken newAuthorizationTokenUser =
		//		new UserAuthorizationToken(dateStart, dateFinish, sessionKey, status, user);

		//	int? createResult = _dbContext.UserAuthorizationsToken.Create(newAuthorizationTokenUser);

		//	if (!createResult.HasValue)
		//		return false;

		//	#endregion

		//	#region Обнулить старый ключ

		//	AuthorizationStatus oldStatus = 
		//		new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active);

		//	AuthorizationStatus newStatus =
		//		new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.BlockNewAuthorization);

		//	DateTime dateBlock = DateTime.Today;

		//	bool cancelResult = _dbContext.UserAuthorizationsToken.CancelSessionKeys(user, oldStatus, dateBlock, newStatus);

		//	if (cancelResult)
		//		return true;

		//	#endregion

		//	return false;

		//}


	}
}
