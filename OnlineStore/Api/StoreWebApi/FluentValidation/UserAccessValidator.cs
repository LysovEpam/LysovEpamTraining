namespace StoreWebApi.FluentValidation
{
	public class UserAccessValidator
	{
		#region Статические свойства полей класса

		public static int LoginMaxLength { get; } = 50;
		public static int PasswordHashLength { get; } = 50;
		public static int StatusMaxLength { get; } = 50;
		public static int RoleMaxLength { get; } = 50;

		public static int PasswordMinLength { get; } = 6;
		public static int PasswordMaxLength { get; } = 50;
		public static int LoginMinLength { get; } = 3;
		public static int StatusMinLength { get; } = 3;
		public static int RoleMinLength { get; } = 3;


		#endregion
		#region Проверка корректности полей модели

		//public static (bool, string) ValidLogin(string login)
		//{
		//	(bool, string) result = (true, null);


		//	if (string.IsNullOrEmpty(login))
		//		result = (false, $"{nameof(Login)} must not be empty");
		//	else if (login.Length < LoginMinLength)
		//		result = (false, $"{nameof(Login)}  must not be shorter than {LoginMinLength} characters");
		//	else if (login.Length > LoginMaxLength)
		//		result = (false, $"{nameof(Login)}  must not exceed {LoginMaxLength} characters");

		//	return result;
		//}

		//public static (bool, string) ValidPassword(string password)
		//{
		//	(bool, string) result = (true, null);

		//	if (string.IsNullOrEmpty(password))
		//		result = (false, $"{nameof(Login)} must not be empty");
		//	else if (password.Length < PasswordMinLength)
		//		result = (false, $"{nameof(Login)}  must not be shorter than {PasswordMinLength} characters");

		//	return result;
		//}

		//public static (bool, string) ValidPasswordHash(string passwrodHash)
		//{
		//	(bool, string) result = (true, null);

		//	if (string.IsNullOrEmpty(passwrodHash))
		//		result = (false, $"{nameof(PasswordHash)} must not be empty");
		//	else if (passwrodHash.Length != PasswordHashLength)
		//		result = (false, $"{nameof(PasswordHash)} must not exceed {PasswordHashLength} characters");

		//	return result;
		//}
		//public static (bool, string) ValidStatus(string status)
		//{
		//	(bool, string) result = (true, null);


		//	if (string.IsNullOrEmpty(status))
		//		result = (false, $"{nameof(Status)} must not be empty");
		//	else if (status.Length < StatusMinLength)
		//		result = (false, $"{nameof(Status)}  must not be shorter than {StatusMinLength} characters");
		//	else if (status.Length > StatusMaxLength)
		//		result = (false, $"{nameof(Status)}  must not exceed {StatusMaxLength} characters");


		//	return result;
		//}
		//public static (bool, string) ValidRole(string role)
		//{
		//	(bool, string) result = (true, null);


		//	if (string.IsNullOrEmpty(role))
		//		result = (false, $"{nameof(Role)} must not be empty");
		//	else if (role.Length < RoleMinLength)
		//		result = (false, $"{nameof(Role)}  must not be shorter than {RoleMinLength} characters");
		//	else if (role.Length > StatusMaxLength)
		//		result = (false, $"{nameof(Role)}  must not exceed {RoleMaxLength} characters");


		//	return result;
		//}

		#endregion
	}
}
