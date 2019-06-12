using CommonEntities;
using FluentValidation;

namespace StoreWebApi.Models.FluentValidation
{
	public class ProductInformationValidator : AbstractValidator<ProductInformation>
	{
		public ProductInformationValidator()
		{

			RuleFor(product => product.ProductName).NotNull().NotEmpty()
				.WithMessage($"{nameof(ProductInformation.ProductName)} must not be empty");

			RuleFor(product => product.ProductName)
				.Length(ProductInformation.ProductNameMinLength, ProductInformation.ProductNameMaxLength).WithMessage(
					$"{nameof(ProductInformation.ProductName)}  must not exceed {ProductInformation.ProductNameMaxLength} characters");


			RuleFor(product => product.Description).NotNull().NotEmpty()
				.WithMessage($"{nameof(ProductInformation.Description)} must not be empty");

			RuleFor(product => product.Description)
				.Length(ProductInformation.DescriptionMinLength, ProductInformation.DescriptionMaxLength).WithMessage(
					$"{nameof(ProductInformation.Description)}  must not exceed {ProductInformation.DescriptionMaxLength} characters");


			RuleFor(product => product.ImageLocalSource).NotNull().NotEmpty()
				.WithMessage($"{nameof(ProductInformation.ImageLocalSource)} must not be empty");

			RuleFor(product => product.ImageLocalSource)
				.Length(ProductInformation.ImageLocalSourceMinLength, ProductInformation.ImageLocalSourceMaxLength).WithMessage(
					$"{nameof(ProductInformation.ImageLocalSource)}  must not exceed {ProductInformation.ImageLocalSourceMaxLength} characters");

		}
	}
}
