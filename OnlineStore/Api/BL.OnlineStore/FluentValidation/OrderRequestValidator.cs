using System.Linq;
using BLContracts.Models;
using CommonEntities.Additional;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class OrderRequestValidator : AbstractValidator<OrderRequest>
	{
		public OrderRequestValidator()
		{
			RuleFor(order => order.Address)
				.NotNull().NotEmpty().WithMessage("Address name must not be empty. ");

			RuleFor(order => order.Address)
				.Length(1, 500).WithMessage("Address name  must have 2-500 characters. ");



			RuleFor(order => order.Status)
				.Must(CheckStatus).WithMessage("Status must not be empty. ");




			RuleFor(order => order.UserLogin)
				.NotNull().NotEmpty().WithMessage("User login must not be empty. ");

			RuleFor(order => order.UserLogin)
				.Length(1, 50).WithMessage("User login  must have 1-50 characters. ");


			RuleFor(product => product.IdProducts)
				.Must(list => list.Any()).WithMessage("Order must have at least one product. ");




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
