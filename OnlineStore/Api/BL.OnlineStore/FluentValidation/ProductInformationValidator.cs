using CommonEntities;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class ProductInformationValidator : AbstractValidator<ProductInformation>
	{
		
		
		
		public ProductInformationValidator()
		{

			RuleFor(product => product.ProductName).NotNull().NotEmpty()
				.WithMessage("Product name must not be empty. ");

			RuleFor(product => product.ProductName).Length(1, 50)
				.WithMessage("Product name  must have 2-50 characters. ");



			RuleFor(product => product.Description).NotNull().NotEmpty()
				.WithMessage("Description must not be empty. ");

			RuleFor(product => product.Description).Length(0, 500)
				.WithMessage("Description  must not exceed 500 characters. ");



			RuleFor(product => product.ImageLocalSource).NotNull().NotEmpty()
				.WithMessage("Image source must not be empty. ");

			RuleFor(product => product.ImageLocalSource).Length(1, 500).
				WithMessage("Image source must not exceed 500 characters. ");


			RuleFor(product => product.ProductCategories).Must(list => list.Count > 0)
				.WithMessage("Product information must have at least one category. ");





		}
	}
}
