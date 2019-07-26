using System;
using System.Collections.Generic;
using BLContracts.Models;
using CommonEntities.Additional;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class ProductSearchRequestValidator : AbstractValidator<ProductSearchRequest>
	{
		public ProductSearchRequestValidator()
		{
			
			RuleFor(request => request.ProductStatuses)
				.Must(CheckStatus).WithMessage("Status must not be empty. ");
		}

		private bool CheckStatus(IEnumerable<string> productStatuses)
		{

			if (productStatuses == null)
				return false;

			try
			{
				foreach (var status in productStatuses)
				{
					var unused = new ProductStatus(status);
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}
	}
}
