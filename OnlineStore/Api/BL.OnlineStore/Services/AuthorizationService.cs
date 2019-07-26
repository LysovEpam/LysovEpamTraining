using System;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services
{
	public class AuthorizationService : IAuthorizationService
	{

		private readonly IDbContext _dbContext;
		private readonly IPasswordHash _passwordHash;
		private readonly ISessionTokenGenerate _sessionTokenGenerator;
		
		private readonly TimeSpan _timeKeyWork;

		public AuthorizationService(IDbContext dbContext, IPasswordHash passwordHash, ISessionTokenGenerate sessionTokenGenerator)
		{
			_dbContext = dbContext;
			_passwordHash = passwordHash;
			_sessionTokenGenerator = sessionTokenGenerator;

			
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

			var sessionToken = _sessionTokenGenerator.GenerateSessionToken(authorizationData.Login);

			UserAuthorizationToken userToken = new UserAuthorizationToken(DateTime.Now, DateTime.Now.Add(_timeKeyWork),
				sessionToken, new AuthorizationStatus(AuthorizationStatus.AuthorizationStatusEnum.Active),
				// ReSharper disable once PossibleInvalidOperationException
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



		



		

	}
}
