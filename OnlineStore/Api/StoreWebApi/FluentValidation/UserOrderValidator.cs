using CommonEntities;
using FluentValidation;

namespace StoreWebApi.FluentValidation
{
	public class UserOrderValidator : AbstractValidator<UserOrder>
	{
		public UserOrderValidator()
		{
		}

		public static int AddressMaxLength { get; } = 500;
		public static int AddressMinLength { get; } = 1;

		public static int StatusMaxLength { get; } = 50;
		public static int StatusMinLength { get; } = 1;
	}
	
}
