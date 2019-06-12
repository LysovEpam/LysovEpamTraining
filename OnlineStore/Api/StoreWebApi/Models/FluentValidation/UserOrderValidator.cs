using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonEntities;
using FluentValidation;

namespace StoreWebApi.Models.FluentValidation
{
	public class UserOrderValidator : AbstractValidator<UserOrder>
	{
		public UserOrderValidator()
		{
			
		}
	}
}
