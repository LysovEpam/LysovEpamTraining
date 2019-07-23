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
	public class RegistrationService : IRegistrationService
	{
		private readonly IPasswordHash _passwordHash;
		private readonly IDbContext _dbContext;

		public RegistrationService(IPasswordHash passwordHash, IDbContext dbContext)
		{
			_passwordHash = passwordHash;
			_dbContext = dbContext;
		}

		public ServiceResult CreateNewUser(RegistrationRequest registrationData)
		{

			#region Проверить уникальность логина

			bool loginIsUnique = _dbContext.UserAdmittances.LoginUserIsUnique(registrationData.Login);

			if (!loginIsUnique)
			{
				var typeError = ServiceResult.ResultConnectionEnum.InvalidRequestData;
				var resultError = new ServiceResult(typeError, "User with the same name already exists!");
				return resultError;
			}

			#endregion

			string passwordHash;

			passwordHash = _passwordHash.GeneratePasswordHash(registrationData.Login, registrationData.Password);

			var userStatus = new UserStatus(UserStatus.StatusEnum.Active);
			var userRole = new UserRole(UserRole.RoleEnum.User);

			var userAdmittance = new UserAdmittance(registrationData.Login, passwordHash, userStatus, userRole);
			
			
			var systemUser = new UserSystem
			{
				FirsName = registrationData.FirstName,
				LastName = registrationData.LastName,
				Phone = registrationData.Phone,
				Email = registrationData.Email,
				UserAdmittance = userAdmittance
			};

			
			bool userIsSave = SaveNewUser(systemUser);

			if (!userIsSave)
			{
				var typeError = ServiceResult.ResultConnectionEnum.SystemError;
				var resultError = new ServiceResult(typeError, "Registration failed, please try again");
				return resultError;
			}

			var typeResult = ServiceResult.ResultConnectionEnum.Correct;
			var result = new ServiceResult(typeResult, "Registration completed");
			return result;

			
		}

		private bool SaveNewUser(UserSystem userSystem)
		{

			try
			{
				int? idUserAdmittance = _dbContext.UserAdmittances.Insert(userSystem.UserAdmittance);

				if (!idUserAdmittance.HasValue || idUserAdmittance < 1)
					return false;

				userSystem.UserAdmittanceId = idUserAdmittance.Value;
				
				_dbContext.UsersSystem.Insert(userSystem);
				return true;

			}
			catch (Exception)
			{
				return false;
			}

		}



	}
}
