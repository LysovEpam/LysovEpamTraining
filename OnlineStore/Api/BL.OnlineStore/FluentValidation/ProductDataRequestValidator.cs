using BLContracts.Models;
using CommonEntities.Additional;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class ProductDataRequestValidator : AbstractValidator<ProductDataRequest>
	{

		public ProductDataRequestValidator()
		{
			RuleFor(product => product.Status)
				.Must(CheckStatus).WithMessage("Status must not be empty. ");

			RuleFor(product => product.IdProductInformation)
				.NotNull().Must(id => id > 0).WithMessage("Product information must not be empty. ");

			RuleFor(product => product.Price)
				.Must(price => price > 0).WithMessage("Status must not be empty. ");

		}

		private bool CheckStatus(string statusName)
		{
			try
			{
				ProductStatus unused = new ProductStatus(statusName);
				return true;
			}
			catch
			{
				return false;
			}

		}
	}
}
