using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services
{
	[SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
	public class UserSystemService : IUserSystemService
	{
		private readonly IPasswordHash _passwordHash;
		private readonly IDbContext _dbContext;

		public UserSystemService(IPasswordHash passwordHash, IDbContext dbContext)
		{
			_passwordHash = passwordHash;
			_dbContext = dbContext;
		}

		public (ServiceResult actionResult, IEnumerable<SystemUserData> userData) GetAll()
		{
			var allUsers = _dbContext.UsersSystem.SelectAll();

			if (allUsers == null)
			{
				ServiceResult resultError = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "Users not found");
				return (resultError, null);
			}

			var listResult = new List<SystemUserData>();

			foreach (var user in allUsers)
			{
				var userData = MapUserToData(user);
				if (userData != null)
					listResult.Add(userData);
			}

			ServiceResult resultCorrect = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "Correct request");
			return (resultCorrect, listResult);

		}
		public (ServiceResult actionResult, SystemUserData userData) GetById(int id)
		{
			var user = _dbContext.UsersSystem.SelectById(id);

			var userDataResult = MapUserToData(user);

			var serviceResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "Correct");

			return (serviceResult, userDataResult);

		}
		public (ServiceResult actionResult, SystemUserData userData) GetByToken(string sessionToken)
		{
			var user = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			var userDataResult = MapUserToData(user.UserSystem);

			var serviceResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "Correct");

			return (serviceResult, userDataResult);
		}
		public ServiceResult Create(string sessionToken, SystemUserData userData)
		{

			#region Check the administrator password is correct


			var user = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			string login = user.UserSystem.UserAdmittance.Login;
			string password = userData.OldPassword;

			string newHash = _passwordHash.GeneratePasswordHash(login, password);
			string oldHash = user.UserSystem.UserAdmittance.PasswordHash;

			if (newHash != oldHash)
			{
				ServiceResult errorResult = new ServiceResult(ServiceResult.ResultConnectionEnum.AccessDenied,
					"To create a new user, you must confirm the current password.");
				return errorResult;
			}


			#endregion

			return CreatByRegistration(userData);

		}
		public ServiceResult CreatByRegistration(SystemUserData userData)
		{
			

			#region Check uniq login

			bool loginIsUnique = _dbContext.UserAdmittances.LoginUserIsUnique(userData.Login);

			if (!loginIsUnique)
			{
				var typeError = ServiceResult.ResultConnectionEnum.InvalidRequestData;
				var resultError = new ServiceResult(typeError, "User with the same name already exists!");
				return resultError;
			}

			#endregion

			
			string passwordHash;

			passwordHash = _passwordHash.GeneratePasswordHash(userData.Login, userData.NewPassword);

			var userStatus = new UserStatus(userData.Status);
			var userRole = new UserRole(userData.Role);

			var userAdmittance = new UserAdmittance(userData.Login, passwordHash, userStatus, userRole);

			var systemUser = new UserSystem
			{
				FirsName = userData.FirstName,
				LastName = userData.LastName,
				Phone = userData.Phone,
				Email = userData.Email,
				UserAdmittance = userAdmittance
			};

			bool userIsSave;

			#region Save new user


			int? idUserAdmittance = _dbContext.UserAdmittances.Insert(systemUser.UserAdmittance);

			if (idUserAdmittance.HasValue && idUserAdmittance.Value > 0)
			{
				systemUser.UserAdmittanceId = idUserAdmittance.Value;

				int? idUser = _dbContext.UsersSystem.Insert(systemUser);

				if(idUser.HasValue && idUser > 0)
					userIsSave = true;
				else
				{
					_dbContext.UserAdmittances.Delete(idUserAdmittance.Value);
					userIsSave = false;
				}
			}
			else
			{
				userIsSave = false;
			}


			#endregion

			if (!userIsSave)
			{
				var typeError = ServiceResult.ResultConnectionEnum.SystemError;
				var resultError = new ServiceResult(typeError, "Save new user failed, please try again");
				return resultError;
			}

			var typeResult = ServiceResult.ResultConnectionEnum.Correct;
			var result = new ServiceResult(typeResult, "Save new user completed");
			return result;
		}

		public ServiceResult Update(string sessionToken, SystemUserData userData)
		{
			#region Check access

			var userAccess = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			if (userAccess.UserSystem.UserAdmittance.Login != userData.Login &&
				userAccess.UserSystem.UserAdmittance.UserRole.Role != UserRole.RoleEnum.Admin)
			{
				string messageError = "Only user and administrator can change user information. ";
				var resultError = new ServiceResult(ServiceResult.ResultConnectionEnum.AccessDenied, messageError);
				return resultError;
			}


			if (userAccess.UserSystem.IdEntity != null && userAccess.UserSystem.IdEntity.Value == userData.IdUser)
			{
				if (userAccess.UserSystem.UserAdmittance.UserRole.Role != new UserRole(userData.Role).Role ||
					userAccess.UserSystem.UserAdmittance.UserStatus.Status != new UserStatus(userData.Status).Status)
				{
					string messageError = "Information about this user can be changed only through the user's personal account.";
					var resultError = new ServiceResult(ServiceResult.ResultConnectionEnum.AccessDenied, messageError);
					return resultError;
				}

			}

			string checkHash = _passwordHash.GeneratePasswordHash(userAccess.UserSystem.UserAdmittance.Login, userData.OldPassword);
			if (checkHash != userAccess.UserSystem.UserAdmittance.PasswordHash)
			{
				ServiceResult errorResult = new ServiceResult(ServiceResult.ResultConnectionEnum.AccessDenied,
					"To update user information, you must confirm the current password. ");
				return errorResult;
			}

			#endregion

			var user = _dbContext.UsersSystem.GetUserByLogin(userData.Login);

			user.FirsName = userData.FirstName;
			user.LastName = userData.LastName;
			user.Email = userData.Email;
			user.Phone = userData.Phone;
			user.UserAdmittance.UserRole = new UserRole(userData.Role);
			user.UserAdmittance.UserStatus = new UserStatus(userData.Status);

			if (!string.IsNullOrEmpty(userData.NewPassword))
				user.UserAdmittance.PasswordHash = _passwordHash.GeneratePasswordHash(userData.Login, userData.NewPassword);

			bool updateResultAdmittances;
			bool updateResultUser = false;

			updateResultAdmittances = _dbContext.UserAdmittances.Update(user.UserAdmittance);

			if(updateResultAdmittances)
				updateResultUser = _dbContext.UsersSystem.Update(user);

			if (!updateResultAdmittances || !updateResultUser)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "Failed to save user information.");
			

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "User information successfully changed");

		}


		public ServiceResult Delete(string sessionToken, int id)
		{
			var userByToken = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);
			
			if (userByToken.UserSystem.IdEntity.Value == id)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"You can not delete yourself!");
			}

			
			var orders = _dbContext.UserOrders.Find(order => order.UserSystem.IdEntity.Value == id);

			if (orders != null && orders.Count != 0)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"User cannot be deleted because it is associated with order.");
			}

			var tokens = _dbContext.UserAuthorizationsToken.Find(token => token.UserId == id);

			if (tokens != null)
			{
				foreach (var token in tokens)
				{
					_dbContext.UserAuthorizationsToken.Delete(token.IdEntity.Value);
				}
			}

			var user = _dbContext.UsersSystem.SelectById(id);

			_dbContext.UsersSystem.Delete(id);
			_dbContext.UserAdmittances.Delete(user.UserAdmittance.IdEntity.Value);

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct,
				"User delete");
		}



		private SystemUserData MapUserToData(UserSystem user)
		{
			if (user == null)
				return null;

			SystemUserData result = new SystemUserData
			{
				IdUser = user.IdEntity.Value,
				FirstName = user.FirsName,
				LastName = user.LastName,
				Phone = user.Phone,
				Email = user.Email,
				Login = user.UserAdmittance.Login,
				OldPassword = "",
				NewPassword = "",
				Role = user.UserAdmittance.UserRole.GetRoleName(),
				Status = user.UserAdmittance.UserStatus.GetStatusName()
			};

			return result;
		}





		
	}
}
