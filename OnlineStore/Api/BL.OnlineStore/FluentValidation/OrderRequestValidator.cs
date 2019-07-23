using System.Linq;
using BLContracts.ActionResults;
using BLContracts.Models;
using FluentValidation;

namespace BL.OnlineStore.FluentValidation
{
	public class OrderRequestValidator : AbstractValidator<OrderData>
	{
		public OrderRequestValidator()
		{
			//RuleFor(order => order.Address).NotNull().NotEmpty()
			//	.WithMessage("Address name must not be empty.");

			//RuleFor(order => order.Address).Length(1, 500)
			//	.WithMessage("Address name  must have 2-500 characters.");



			//RuleFor(order => order.Status).NotNull().NotEmpty()
			//	.WithMessage("Status must not be empty.");

			//RuleFor(order => order.Status).Length(1, 50)
			//	.WithMessage("Status name  must have 1-50 characters.");



			//RuleFor(order => order.UserLogin).NotNull().NotEmpty()
			//	.WithMessage("User login must not be empty");

			//RuleFor(order => order.UserLogin).Length(1, 50)
			//	.WithMessage("User login  must have 1-50 characters.");


			//RuleFor(product => product.IdProducts).Must(list => list.Any())
			//	.WithMessage("Order must have at least one product");




		}


	}
	
}
