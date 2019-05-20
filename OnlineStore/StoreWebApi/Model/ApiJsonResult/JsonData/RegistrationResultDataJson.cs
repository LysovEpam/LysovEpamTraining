using System.Runtime.Serialization;

namespace StoreWebApi.Model.ApiJsonResult.JsonData
{
	[DataContract]
	public class RegistrationResultDataJson
	{
		[DataMember]
		public bool LoginIsCorrect { get; }
		[DataMember]
		public string LoginErrorText { get; }

		[DataMember]
		public bool PasswordIsCorrect { get; }
		[DataMember]
		public string PasswordErrorText { get; }

		[DataMember]
		public bool FirstNameIsCorrect { get; }
		[DataMember]
		public string FirstNameErrorText { get; }

		[DataMember]
		public bool LastNameIsCorrect { get; }
		[DataMember]
		public string LastNameErrorText { get; }

		[DataMember]
		public bool EmailIsCorrect { get; }
		[DataMember]
		public string EmailErrorText { get; }

		[DataMember]
		public bool PhoneIsCorrect { get; }
		[DataMember]
		public string PhoneErrorText { get; }

		public RegistrationResultDataJson(
			bool loginIsCorrect, string loginErrorText,
			bool passwordIsCorrect, string passwordErrorText,
			bool firstNameIsCorrect, string firstNameErrorText,
			bool lastNameIsCorrect, string lastNameErrorText,
			bool emailIsCorrect, string emailErrorText,
			bool phoneIsCorrect, string phoneErrorText)
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
	}
}
