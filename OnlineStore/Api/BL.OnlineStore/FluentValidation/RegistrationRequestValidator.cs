using BLContracts.Models;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
	{
		public RegistrationRequestValidator()
		{

			RuleFor(request => request != null)
				.NotNull().WithMessage("Request must not be empty. ");
			
			
			RuleFor(request => request.Login)
				.NotNull().NotEmpty().WithMessage("Login must not be empty. ")
				.Length(3, 50).WithMessage("Login must have 3-50 characters. ")
				.Must(CheckContainSpaces).WithMessage("Login must not contain spaces. ");

			
			RuleFor(request => request.FirstName)
				.NotNull().NotEmpty().WithMessage("First name must not be empty. ")
				.Length(2, 50).WithMessage("First name must have 2-50 characters. ");

			RuleFor(request => request.LastName)
				.NotNull().NotEmpty().WithMessage("Last name must not be empty. ")
				.Length(2, 50).WithMessage("Last name must have 2-50 characters. ");

			RuleFor(request => request.Email)
				.NotNull().NotEmpty().WithMessage("Email must not be empty. ")
				.Length(2, 50).WithMessage("Email name must have 2-50 characters. ")
				.EmailAddress().WithMessage("Invalid email address");

			RuleFor(request => request.Phone)
				.NotNull().NotEmpty().WithMessage("Phone must not be empty. ")
				.Length(6, 50).WithMessage("Phone name must have 6-50 characters. ");



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
