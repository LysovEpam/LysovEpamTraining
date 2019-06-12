using BLContracts;
using BLContracts.ActionResults;
using BLContracts.SystemBl;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.BlModels.SystemBlModels
{
	public class AuthorizationBlModel : BaseBlModel, IAuthorizationBlModel
	{

		private readonly IDbContext _dbContext;
		private readonly IPasswordHash _passwordHash;

		public AuthorizationBlModel(IDbContext dbContext, IPasswordHash passwordHash, IProgramLogRegister logRegister)
			: base(logRegister)
		{
			_dbContext = dbContext;
			_passwordHash = passwordHash;
		}

		public BaseActionResult CheckAuthorization(string login, string password)
		{
			return CheckAccess(login, password);
		}

		
		public UserRole GetUserRole(string login, string password)
		{
			BaseActionResult actionResult = CheckAccess(login, password);

			if (actionResult.ResultConnection != BaseActionResult.ResultConnectionEnum.Correct)
				return null;


			UserAccess userAccess = GetUserAccess(login, password);

			return userAccess?.UserRole;
		}


		private BaseActionResult CheckAccess(string login, string password)
		{
			UserAccess userAccess = GetUserAccess(login, password);

			BaseActionResult result = null;

			if (userAccess == null)
			{
				result = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Пользователь с таким логином и паролем не найден");
			}
			else if (userAccess.UserStatus.Status == UserStatus.StatusEnum.Active)
			{
				result = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct,
					"");
			}
			else if (userAccess.UserStatus.Status == UserStatus.StatusEnum.Block)
			{
				result = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Пользователь заблокирован");
			}
			else if (userAccess.UserStatus.Status == UserStatus.StatusEnum.Delete)
			{
				result = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Пользователь удален");
			}

			return result;
		}

		private UserAccess GetUserAccess(string login, string passwrod)
		{
			string hash = _passwordHash.GeneratePasswordHash(login, passwrod);

			UserAccess userAccess = _dbContext.UserAccesses.GetUserAccess(login, hash);

			return userAccess;
		}


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
