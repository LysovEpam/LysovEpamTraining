using BLContracts.Models;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class SystemUserDataValidator : AbstractValidator<SystemUserData>
	{
		public SystemUserDataValidator()
		{
			RuleFor(request => request != null)
				.NotNull().WithMessage("Request must not be empty");
			
			RuleFor(request => request.FirstName)
				.NotNull().WithMessage("First name must not be empty")
				.NotEmpty().WithMessage("First name must not be empty")
				.Length(2, 50).WithMessage("First name must have 2-50 characters");

			RuleFor(request => request.LastName)
				.NotNull().WithMessage("Last name must not be empty")
				.NotEmpty().WithMessage("Last name name must not be empty")
				.Length(2, 50).WithMessage("Last name must have 2-50 characters");

			RuleFor(request => request.Email)
				.NotNull().WithMessage("Email must not be empty")
				.NotEmpty().WithMessage("Email name must not be empty")
				.Length(2, 50).WithMessage("Email name must have 2-50 characters")
				.EmailAddress().WithMessage("Invalid email address");

			RuleFor(request => request.Phone)
				.NotNull().WithMessage("Phone must not be empty")
				.NotEmpty().WithMessage("Phone name must not be empty")
				.Length(6, 50).WithMessage("Phone name must have 6-50 characters");

			RuleFor(request => request.Role)
				.NotNull().WithMessage("Role must not be empty")
				.NotEmpty().WithMessage("Role name must not be empty")
				.Length(1, 50).WithMessage("Role name must have 1-50 characters");

			RuleFor(request => request.Status)
				.NotNull().WithMessage("Status must not be empty")
				.NotEmpty().WithMessage("Status name must not be empty")
				.Length(1, 50).WithMessage("Status name must have 1-50 characters");

			RuleFor(request => request.NewPassword)
				.NotNull().WithMessage("Password must not be empty")
				.NotEmpty().WithMessage("Passwrod must not be empty")
				.Length(8, 50).WithMessage("Passwrod must have 8-50 characters")
				.Must(CheckContainSpaces).WithMessage("Password must not contain spaces");


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
