using System;
using System.Collections.Generic;
using System.Linq;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.ActionResults.System;
using BLContracts.SystemBl;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.SystemBlModel
{
	public class RegistrationBlModel : IRegistrationBlModel
	{
		private readonly IPasswordHash _passwordHash;
		private readonly IDbContext _dbContext;
		public RegistrationBlModel(IPasswordHash passwordHash, IDbContext dbContext)
		{
			_passwordHash = passwordHash;
			_dbContext = dbContext;
		}

		public RegistrationResult CreateNewUser(string firstName, string lastName, string email, string phone, string login, string password)
		{
			#region Проверка входных параметров

			var validLogin = UserAccess.ValidLogin(login);
			var validPassrod = UserAccess.ValidPassword(password);
			var validFirstName = User.ValidFirstName(firstName);
			var validLastName = User.ValidLastName(lastName);
			var validEmail = User.ValidEmail(email);
			var validPhone = User.ValidPhone(phone);

			if (!validLogin.Item1 || !validPassrod.Item1 ||
				!validFirstName.Item1 || !validLastName.Item1 ||
				!validEmail.Item1 || !validPhone.Item1)
			{
				BaseActionResult.ResultConnectionEnum type = BaseActionResult.ResultConnectionEnum.SystemError;
				string message = "Введены некорректные данные для регистрации пользователя";

				RegistrationResult resultError = new RegistrationResult(type, message, null,
					validLogin.Item1, validLogin.Item2, validPassrod.Item1, validPassrod.Item2,
					validFirstName.Item1, validFirstName.Item2, validLastName.Item1, validLastName.Item2,
					validEmail.Item1, validEmail.Item2, validPhone.Item1, validPhone.Item2);

				return resultError;
			}

			#endregion

			#region Проверить уникальность логина

			bool loginIsUnique = _dbContext.UserAccesses.LoginUserIsUnique(login);

			if (!loginIsUnique)
			{
				BaseActionResult.ResultConnectionEnum type = BaseActionResult.ResultConnectionEnum.SystemError;
				string message = "Пользователь с таким именем уже существует!";
				RegistrationResult resultError = new RegistrationResult(type, message);
				return resultError;
			}

			#endregion

			string passwordHash;


			#region Создать хэш пароля


			try
			{
				passwordHash = _passwordHash.GeneratePasswordHash(login, password);
			}
			catch (ArgumentException)
			{
				//ЗАМЕНИТЬ! Добавить обработку исключения!
				passwordHash = null;
			}
			catch (Exception)
			{
				//ЗАМЕНИТЬ! Добавить обработку исключения!
				passwordHash = null;
			}


			if (string.IsNullOrEmpty(passwordHash))
			{
				BaseActionResult.ResultConnectionEnum type = BaseActionResult.ResultConnectionEnum.SystemError;
				string message = "Введены некорректные данные для доступа (Логин + пароль)";

				RegistrationResult resultError = new RegistrationResult(type, message, null,
					false, "Введены некорректные данные для доступа",
					false, "Введены некорректные данные для доступа",
					validFirstName.Item1, validFirstName.Item2,
					validLastName.Item1, validLastName.Item2,
					validEmail.Item1, validEmail.Item2,
					validPhone.Item1, validPhone.Item2);

				return resultError;
			}

			#endregion

			UserStatus userStatus = new UserStatus(UserStatus.StatusEnum.Active);
			UserRole userRole = new UserRole(UserRole.RoleEnum.User);

			UserAccess userAccess = new UserAccess(login, passwordHash, userStatus, userRole);

			User user;

			#region Создание модели пользователя

			try
			{
				user = new User(firstName, lastName, email, phone, userAccess);
			}
			catch (Exception)
			{
				BaseActionResult.ResultConnectionEnum type = BaseActionResult.ResultConnectionEnum.SystemError;
				string message = "Не удалось создать пользователя по введенным данным, попробуйте повторить попытку";
				RegistrationResult resultError = new RegistrationResult(type, message);
				return resultError;
			}

			#endregion


			return new RegistrationResult(BaseActionResult.ResultConnectionEnum.Correct, "", user);

		}

		public BaseActionResult SaveNewUser(User user)
		{

			if (user == null)
				return new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
				"Не удалось сохранить информацию о пользователе");

			try
			{
				_dbContext.Users.Create(user);
				return new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			}
			catch (Exception)
			{
				return new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось сохранить информацию о пользователе");
			}

		}



	}
}
