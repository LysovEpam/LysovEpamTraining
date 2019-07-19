using System;
using BLContracts.ActionResults;
using BLContracts.MainService;
using BLContracts.Models;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services.MainServices
{
	public class UserSystemService :  IUserSystemService
	{
		private readonly IDbContext _dbContext;

		public UserSystemService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public (ServiceResult actionResult, SystemUserData systemUserData) GetUserInformation(string sessionToken, string login)
		{
			var userByToken = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);


			if (userByToken.UserSystem.UserAdmittance.Login.ToLower() != login.ToLower())
			{
				return (new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Unable to place an order. User authorization failed. Try logging in again."), null);
			}

			

			SystemUserData dataResult = new SystemUserData
			{
				Login = login,
				Email = userByToken.UserSystem.Email,
				Phone = userByToken.UserSystem.Phone,
				FirstName = userByToken.UserSystem.FirsName,
				LastName = userByToken.UserSystem.LastName,
				Status = userByToken.UserSystem.UserAdmittance.UserStatus.GetStatusName(),
				Role = userByToken.UserSystem.UserAdmittance.UserRole.GetRoleName()
			};

			return (new ServiceResult(ServiceResult.ResultConnectionEnum.Correct,""), dataResult);

		}

		public ServiceResult UpdateUserByMyself(string sessionToken, SystemUserData userData)
		{
			var userByToken = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			if (userByToken.UserSystem.UserAdmittance.Login != userData.Login)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Unable to place an order. User authorization failed. Try logging in again.");
			}
			

			var user = userByToken.UserSystem;

			user.Email = userData.Email;
			user.Phone = userData.Phone;
			user.FirsName = userData.FirstName;
			user.LastName = userData.LastName;

			var updateResult = _dbContext.UsersSystem.Update(user);

			if(!updateResult)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Unable to change user information.");

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");


		}
	}
}
