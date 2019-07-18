using BLContracts.Models;
using CommonEntities;
using FluentValidation;

namespace StoreWebApi.FluentValidation
{
	public class ProductDataRequestValidator : AbstractValidator<ProductDataRequest>
	{
		
		
		public ProductDataRequestValidator()
		{
			RuleFor(product => product.Status).NotNull()
				.WithMessage($"Status must not be empty");

			RuleFor(product => product.Status).Length(1, 50)
				.WithMessage("Status must not exceed 50 characters");

			RuleFor(product => product.IdProductInformation).NotNull().Must(id => id > 0)
				.WithMessage("Product information must not be empty");

		}
	}
}
