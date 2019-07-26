using CommonEntities;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class ProductCategoryValidator : AbstractValidator<ProductCategory>
	{
		public ProductCategoryValidator()
		{
			RuleFor(category => category.CategoryName)
				.NotNull().NotEmpty().WithMessage("Category name must not be empty. ");

			RuleFor(category => category.CategoryName)
				.Length(2, 50).WithMessage("Category name must have 2-50 characters. ");


			RuleFor(category => category.Description)
				.NotNull().NotEmpty().WithMessage("Description must not be empty. ");

			RuleFor(category => category.Description)
				.Length(1, 500).WithMessage("Description must have 1-500 characters. ");

		}
	}
}
