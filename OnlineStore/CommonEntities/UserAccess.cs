using System;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserAccess : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int LoginMaxLength { get; } = 50;
		public static int PasswordHashLength { get; } = 50;
		public static int StatusMaxLength { get; } = 50;
		public static int RoleMaxLength { get; } = 50;

		public static int PasswordMinLength { get; } = 6;
		public static int PasswordMaxLength { get; } = 50;
		public static int LoginMinLength { get; } = 3;


		#endregion

		#region Свойства класса

		private string _login;
		private string _passwordHash;
		private string _status;
		private string _role;

		private UserStatus _userStatus;
		private UserRole _userRole;


		public string Login
		{
			get => _login;
			set
			{
				(bool, string) validParameter = ValidLogin(value);
				if(validParameter.Item1)
					_login = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(PasswordHash)}");
				
			}
		}
		public string PasswordHash
		{
			get => _passwordHash;
			set
			{
				(bool, string) validParameter = ValidPasswordHash(value);
				if (validParameter.Item1)
					_passwordHash = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(PasswordHash)}");

				_passwordHash = value;
			}
		}
		public string Status
		{
			get => _status;
			set
			{
				(bool, string) validParameter = ValidStatus(value);
				if (validParameter.Item1)
					_status = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(Status)}");

				_status = value;
				_userStatus = new UserStatus(value);
			}
		}
		public string Role
		{
			get => _role;
			set
			{
				(bool, string) validParameter = ValidRole(value);
				if (validParameter.Item1)
					_role = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(Role)}");



				_role = value;
				_userRole = new UserRole(value);
			}
		}



		public UserStatus UserStatus
		{
			get => _userStatus;
			set
			{
				_userStatus = value ?? 
					throw new ArgumentException($"The parameter {nameof(UserStatus)} must not be empty", 
						$"{nameof(UserStatus)}");

				_status = value.GetStatusName();
			}

		}
		public UserRole UserRole
		{
			get => _userRole;
			set
			{
				_userRole = value ??
					throw new ArgumentException($"The parameter {nameof(UserRole)} must not be empty",
						$"{nameof(UserRole)}");

				_role = value.GetRoleName();
			}
		}


		#endregion
		
		#region Конструктор

		public UserAccess()
		{

		}
		public UserAccess(int userAccessId, string login, string passwordHash, string status, string role) : base(userAccessId)
		{
			Login = login;
			PasswordHash = passwordHash;
			Status = status;
			Role = role;
		}
		public UserAccess(int userAccessId, string login, string passwordHash, UserStatus status, UserRole role) : base(userAccessId)
		{
			Login = login;
			PasswordHash = passwordHash;
			UserStatus = status;
			UserRole = role;
		}
		public UserAccess(string login, string passwordHash, UserStatus status, UserRole role)
		{
			Login = login;
			PasswordHash = passwordHash;
			UserStatus = status;
			UserRole = role;
		}

		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidLogin(string login)
		{
			(bool, string) result = (true, null);


			
			if (string.IsNullOrEmpty(login))
				result = (false, $"{nameof(Login)} must not be empty");
			else if (login.Length < LoginMinLength)
				result = (false, $"{nameof(Login)}  must not be shorter than {LoginMinLength} characters");
			else if (login.Length > LoginMaxLength)
				result = (false, $"{nameof(Login)}  must not exceed {LoginMaxLength} characters");

			return result;
		}

		public static (bool, string) ValidPassword(string password)
		{
			(bool, string) result = (true, null);
			
			if (string.IsNullOrEmpty(password))
				result = (false, $"{nameof(Login)} must not be empty");
			else if (password.Length < PasswordMinLength)
				result = (false, $"{nameof(Login)}  must not be shorter than {PasswordMinLength} characters");
			
			return result;
		}

		public static (bool, string) ValidPasswordHash(string passwrodHash)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(passwrodHash))
				result = (false, $"{nameof(PasswordHash)} must not be empty");
			else if (passwrodHash.Length != PasswordHashLength)
				result = (false, $"{nameof(PasswordHash)} must not exceed {PasswordHashLength} characters");

			return result;
		}
		public static (bool, string) ValidStatus(string status)
		{
			(bool, string) result = (true, null);

			int statusMinLength = 3;

			if (string.IsNullOrEmpty(status))
				result = (false, $"{nameof(Status)} must not be empty");
			else if (status.Length < statusMinLength)
				result = (false, $"{nameof(Status)}  must not be shorter than {statusMinLength} characters");
			else if (status.Length > StatusMaxLength)
				result = (false, $"{nameof(Status)}  must not exceed {StatusMaxLength} characters");


			return result;
		}
		public static (bool, string) ValidRole(string role)
		{
			(bool, string) result = (true, null);

			int roleMinLength = 3;

			if (string.IsNullOrEmpty(role))
				result = (false, $"{nameof(Role)} must not be empty");
			else if (role.Length < roleMinLength)
				result = (false, $"{nameof(Role)}  must not be shorter than {roleMinLength} characters");
			else if (role.Length > StatusMaxLength)
				result = (false, $"{nameof(Role)}  must not exceed {RoleMaxLength} characters");


			return result;
		}

		#endregion
	}
}
