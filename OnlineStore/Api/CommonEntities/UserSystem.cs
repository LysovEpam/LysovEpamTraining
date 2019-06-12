using System;

namespace CommonEntities
{
	public class UserSystem : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int FirstNameMaxLength { get; } = 50;
		public static int LastNameMaxLength { get; } = 50;
		public static int EmailMaxLength { get; } = 50;
		public static int PhoneMaxLength { get; } = 50;


		#endregion

		#region Свойства класса

		private string _firsName;
		private string _lastName;
		private string _email;
		private string _phone;
		private int _userAccessId;

		private UserAccess _userAccess;



		public string FirsName
		{
			get => _firsName;
			set
			{
				(bool, string) validParameter = ValidFirstName(value);
				if (validParameter.Item1)
					_firsName = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(FirsName)}");

				_firsName = value;
			}
		}
		public string LastName
		{
			get => _lastName;
			set
			{
				(bool, string) validParameter = ValidLastName(value);
				if (validParameter.Item1)
					_lastName = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(LastName)}");

				_lastName = value;
			}
		}
		public string Email
		{
			get => _email;
			set
			{
				(bool, string) validParameter = ValidEmail(value);
				if (validParameter.Item1)
					_email = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(Email)}");


				_email = value;
			}
		}
		public string Phone
		{
			get => _phone;
			set
			{
				(bool, string) validParameter = ValidPhone(value);
				if (validParameter.Item1)
					_phone = value;
				else
					throw new ArgumentException(validParameter.Item2, $"{nameof(Phone)}");


				_phone = value;
			}
		}
		public int UserAccessId
		{
			get => _userAccessId;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(UserAccessId)} mast be more zero", $"{nameof(UserAccessId)}");

				_userAccessId = value;
			}
		}

		public UserAccess UserAccess
		{
			get => _userAccess;
			set
			{
				_userAccess = value;

				if (value?.IdEntity != null)
					UserAccessId = value.IdEntity.Value;
			}
		}


		#endregion

		#region Конструктор

		public UserSystem()
		{

		}
		public UserSystem(int userId, string firsName, string lastName, string email, string phone, int userAccessId) : base(userId)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAccessId = userAccessId;
			UserAccess = null;

		}
		public UserSystem(string firsName, string lastName, string email, string phone, int userAccessId) 
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAccessId = userAccessId;
			UserAccess = null;

		}
		public UserSystem(int userId, string firsName, string lastName, string email, string phone, UserAccess userAccess) : base(userId)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAccess = userAccess;

			if (userAccess?.IdEntity != null)
				UserAccessId = userAccess.IdEntity.Value;
		}
		public UserSystem(string firsName, string lastName, string email, string phone, UserAccess userAccess)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAccess = userAccess;

			if (userAccess?.IdEntity != null)
				UserAccessId = userAccess.IdEntity.Value;
		}

		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidFirstName(string firtName)
		{
			(bool, string) result = (true, null);
			
			int firstNameMinLength = 2;

			if (string.IsNullOrEmpty(firtName))
				result = (false, $"{nameof(FirsName)} must not be empty");
			else if (firtName.Length < firstNameMinLength)
				result = (false, $"{nameof(FirsName)}  must not be shorter than {firstNameMinLength} characters");
			else if (firtName.Length > FirstNameMaxLength)
				result = (false, $"{nameof(FirsName)}  must not exceed {FirstNameMaxLength} characters");

			return result;
		}
		public static (bool, string) ValidLastName(string lastName)
		{
			(bool, string) result = (true, null);

			int lastNameMinLength = 2;

			if (string.IsNullOrEmpty(lastName))
				result = (false, $"{nameof(LastName)} must not be empty");
			else if (lastName.Length < lastNameMinLength)
				result = (false, $"{nameof(LastName)} must not be shorter than {lastNameMinLength} characters");
			else if (lastName.Length > LastNameMaxLength)
				result = (false, $"{nameof(LastName)} must not exceed {LastNameMaxLength} characters");


			return result;
		}
		public static (bool, string) ValidEmail(string email)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(email))
				result = (false, $"{nameof(Email)} must not be empty");
			else if (email.Length > EmailMaxLength)
				result = (false, $"{nameof(Email)}  must not exceed {EmailMaxLength} characters");
			
			return result;
		}
		public static (bool, string) ValidPhone(string phone)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(phone))
				result = (false, $"{nameof(Phone)} must not be empty");
			else if (phone.Length > PhoneMaxLength)
				result = (false, $"{nameof(Phone)}  must not exceed {PhoneMaxLength} characters");
			
			return result;
		}

		#endregion
	}
}
