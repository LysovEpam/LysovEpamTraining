using BLContracts.Models;
using CommonEntities.Additional;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class OrderSearchRequestValidator : AbstractValidator<OrderSearchRequest>
	{
		public OrderSearchRequestValidator()
		{
			RuleFor(request => request.Status)
				.Must(CheckStatus).WithMessage("Status must not be empty. ");

			RuleFor(request => request.SearchString)
				.NotNull().WithMessage("Search string must not be empty. ");

		}

		private bool CheckStatus(string statusName)
		{
			try
			{
				OrderStatus unused = new OrderStatus(statusName);
				return true;
			}
			catch
			{
				return false;
			}

		}
	}
}
