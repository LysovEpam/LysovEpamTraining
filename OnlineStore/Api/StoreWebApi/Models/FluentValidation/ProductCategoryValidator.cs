using CommonEntities;
using FluentValidation;

namespace StoreWebApi.Models.FluentValidation
{
	public class ProductCategoryValidator : AbstractValidator<ProductCategory>
	{
		public ProductCategoryValidator()
		{
			RuleFor(category => category.CategoryName).NotNull().NotEmpty()
				.WithMessage($"{nameof(ProductCategory.CategoryName)} must not be empty");

			RuleFor(category => category.CategoryName).Length(1, ProductCategory.CategoryNameMaxLength).WithMessage(
				$"{nameof(ProductCategory.CategoryName)}  must not exceed {ProductCategory.CategoryNameMaxLength} characters");



			RuleFor(category => category.Description).NotNull().NotEmpty()
				.WithMessage($"{nameof(ProductCategory.Description)} must not be empty");

			RuleFor(category => category.Description).Length(1, ProductCategory.DescriptionMaxLength).WithMessage(
				$"{nameof(ProductCategory.Description)}  must not exceed {ProductCategory.DescriptionMaxLength} characters");

		}
	}
}
