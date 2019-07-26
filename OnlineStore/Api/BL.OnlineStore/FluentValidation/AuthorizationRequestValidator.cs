using BLContracts.Models;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class AuthorizationRequestValidator : AbstractValidator<AuthorizationRequest>
	{
		public AuthorizationRequestValidator()
		{

			RuleFor(request => request.Login)
				.NotNull().NotEmpty().WithMessage("Login must not be empty. ");


			RuleFor(request => request.Password)
				.NotNull().NotEmpty().WithMessage("Password must not be empty. ");

		}

	}
}
