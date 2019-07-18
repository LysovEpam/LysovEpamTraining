
namespace StoreWebApi.FluentValidation
{
	class UserSystemValidator
	{
		#region Статические свойства аргументов класса

		public static int FirstNameMaxLength { get; } = 50;
		public static int LastNameMaxLength { get; } = 50;
		public static int EmailMaxLength { get; } = 50;
		public static int PhoneMaxLength { get; } = 50;


		#endregion
		#region Проверка корректности полей модели

		//public static (bool, string) ValidFirstName(string firtName)
		//{
		//	(bool, string) result = (true, null);

		//	int firstNameMinLength = 2;

		//	if (string.IsNullOrEmpty(firtName))
		//		result = (false, $"{nameof(FirsName)} must not be empty");
		//	else if (firtName.Length < firstNameMinLength)
		//		result = (false, $"{nameof(FirsName)}  must not be shorter than {firstNameMinLength} characters");
		//	else if (firtName.Length > FirstNameMaxLength)
		//		result = (false, $"{nameof(FirsName)}  must not exceed {FirstNameMaxLength} characters");

		//	return result;
		//}
		//public static (bool, string) ValidLastName(string lastName)
		//{
		//	(bool, string) result = (true, null);

		//	int lastNameMinLength = 2;

		//	if (string.IsNullOrEmpty(lastName))
		//		result = (false, $"{nameof(LastName)} must not be empty");
		//	else if (lastName.Length < lastNameMinLength)
		//		result = (false, $"{nameof(LastName)} must not be shorter than {lastNameMinLength} characters");
		//	else if (lastName.Length > LastNameMaxLength)
		//		result = (false, $"{nameof(LastName)} must not exceed {LastNameMaxLength} characters");


		//	return result;
		//}
		//public static (bool, string) ValidEmail(string email)
		//{
		//	(bool, string) result = (true, null);

		//	if (string.IsNullOrEmpty(email))
		//		result = (false, $"{nameof(Email)} must not be empty");
		//	else if (email.Length > EmailMaxLength)
		//		result = (false, $"{nameof(Email)}  must not exceed {EmailMaxLength} characters");

		//	return result;
		//}
		//public static (bool, string) ValidPhone(string phone)
		//{
		//	(bool, string) result = (true, null);

		//	if (string.IsNullOrEmpty(phone))
		//		result = (false, $"{nameof(Phone)} must not be empty");
		//	else if (phone.Length > PhoneMaxLength)
		//		result = (false, $"{nameof(Phone)}  must not exceed {PhoneMaxLength} characters");

		//	return result;
		//}

		#endregion
	}
}
