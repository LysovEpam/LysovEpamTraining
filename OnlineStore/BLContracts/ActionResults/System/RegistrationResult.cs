using CommonEntities;

namespace BLContracts.ActionResults.System
{
	
	public class RegistrationResult : BaseActionResult
	{
		
		public User User { get; set; }

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
			User = null;
			LoginIsCorrect = false;
			LoginErrorText = "";
			PasswordIsCorrect = false;
			PasswordErrorText = "";
			FirstNameIsCorrect = false;
			FirstNameErrorText = "";
			LastNameIsCorrect = false;
			LastNameErrorText = "";
			EmailIsCorrect = false;
			EmailErrorText = "";
			PhoneIsCorrect = false;
			PhoneErrorText = "";
		}


		public RegistrationResult(ResultConnectionEnum resultConnection, string message, User user,
			bool loginIsCorrect, string loginErrorText,
			bool passwordIsCorrect, string passwordErrorText,
			bool firstNameIsCorrect, string firstNameErrorText,
			bool lastNameIsCorrect, string lastNameErrorText,
			bool emailIsCorrect, string emailErrorText,
			bool phoneIsCorrect, string phoneErrorText) :
			base(resultConnection, message)
		{
			User = user;
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



		public RegistrationResult(ResultConnectionEnum resultConnection, string message, User user)
			: base(resultConnection, message)
		{
			User = user;

			LoginIsCorrect = true;
			LoginErrorText = "";
			PasswordIsCorrect = true;
			PasswordErrorText = "";
			FirstNameIsCorrect = true;
			FirstNameErrorText = "";
			LastNameIsCorrect = true;
			LastNameErrorText = "";
			EmailIsCorrect = true;
			EmailErrorText = "";
			PhoneIsCorrect = true;
			PhoneErrorText = "";
		}
	}
}
