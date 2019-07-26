using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class PasswordValidator : AbstractValidator<string>
	{
		public PasswordValidator()
		{
			RuleFor(password => password)
				.NotNull().NotEmpty().WithMessage("Password must not be empty. ")
				.Length(8, 50).WithMessage("Passwrod must have 8-50 characters. ")
				.Must(CheckContainSpaces).WithMessage("Password must not contain spaces. ");
		}

		private bool CheckContainSpaces(string checkString)
		{
			if (checkString == null)
				return false;
			if (checkString.Trim().Length != checkString.Length)
				return false;

			return true;
		}
	}
}
