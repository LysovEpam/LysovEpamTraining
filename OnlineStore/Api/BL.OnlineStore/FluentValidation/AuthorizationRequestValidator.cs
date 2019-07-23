using BLContracts.Models;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class AuthorizationRequestValidator : AbstractValidator<AuthorizationRequest>
	{
		public AuthorizationRequestValidator()
		{
			RuleFor(request => request != null)
				.NotNull().WithMessage("Request must not be empty");

			RuleFor(request => request.Login)
				.NotNull().WithMessage("Login must not be empty")
				.NotEmpty().WithMessage("Login must not be empty");

			RuleFor(request => request.Password)
				.NotNull().WithMessage("Password must not be empty")
				.NotEmpty().WithMessage("Passwrod must not be empty");
		}

	}
}
