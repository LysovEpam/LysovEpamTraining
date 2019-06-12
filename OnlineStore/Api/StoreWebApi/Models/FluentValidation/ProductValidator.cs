using CommonEntities;
using FluentValidation;

namespace StoreWebApi.Models.FluentValidation
{
	public class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			RuleFor(product=> product.Status).NotNull().NotEmpty()
				.WithMessage($"{nameof(Product.Status)} must not be empty");

			RuleFor(product => product.Status)
				.Length(1, Product.ProductStatusMaxLength)
				.WithMessage($"{nameof(Product.Status)}  must not exceed {Product.ProductStatusMaxLength} characters");


			
		}
	}
}
