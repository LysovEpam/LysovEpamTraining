namespace BLContracts.ActionResults.System
{
	
	public class RegistrationResult : BaseActionResult
	{
		public bool LoginIsCorrect { get; set; }
		public string LoginErrorText { get; set; }
		public bool PasswordIsCorrect { get; set; }
		public string PasswordErrorText { get; set; }
		public bool FirstNameIsCorrect { get; set; }
		public string FirstNameErrorText { get; set; }
		public bool LastNameIsCorrect { get; set; }
		public string LastNameErrorText { get; set; }
		public bool EmailIsCorrect { get; set; }
		public string EmailErrorText { get; set; }
		public bool PhoneIsCorrect { get; set; }
		public string PhoneErrorText { get; set; }

		

		public RegistrationResult(ResultConnectionEnum resultConnection, string message) :
			base(resultConnection, message)
		{
			LoginIsCorrect = false;
			LoginErrorText = null;
			PasswordIsCorrect = false;
			PasswordErrorText = null;
			FirstNameIsCorrect = false;
			FirstNameErrorText = null;
			LastNameIsCorrect = false;
			LastNameErrorText = null;
			EmailIsCorrect = false;
			EmailErrorText = null;
			PhoneIsCorrect = false;
			PhoneErrorText = null;
		}


		public RegistrationResult(ResultConnectionEnum resultConnection, string message,
			bool loginIsCorrect, string loginErrorText,
			bool passwordIsCorrect, string passwordErrorText,
			bool firstNameIsCorrect, string firstNameErrorText,
			bool lastNameIsCorrect, string lastNameErrorText,
			bool emailIsCorrect, string emailErrorText,
			bool phoneIsCorrect, string phoneErrorText) :
			base(resultConnection, message)
		{
			LoginIsCorrect = loginIsCorrect;
			LoginErrorText = loginErrorText;
			PasswordIsCorrect = passwordIsCorrect;
			PasswordErrorText = passwordErrorText;
			FirstNameIsCorrect = firstNameIsCorrect;
			FirstNameErrorText = firstNameErrorText;
			LastNameIsCorrect = lastNameIsCorrect;
			LastNameErrorText = lastNameErrorText;
			EmailIsCorrect = emailIsCorrect;
			EmailErrorText = emailErrorText;
			PhoneIsCorrect = phoneIsCorrect;
			PhoneErrorText = phoneErrorText;
		}



		//public RegistrationResult(ResultConnectionEnum resultConnection, string message)
		//	: base(resultConnection, message)
		//{


		//	LoginIsCorrect = true;
		//	LoginErrorText = null;
		//	PasswordIsCorrect = true;
		//	PasswordErrorText = null;
		//	FirstNameIsCorrect = true;
		//	FirstNameErrorText = null;
		//	LastNameIsCorrect = true;
		//	LastNameErrorText = null;
		//	EmailIsCorrect = true;
		//	EmailErrorText = null;
		//	PhoneIsCorrect = true;
		//	PhoneErrorText = null;
		//}
	}
}
