using System.Linq;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using CommonEntities.Additional;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.Models.FluentValidation;

namespace StoreWebApi.Controllers.Main
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserOrderController : Controller
	{
		private readonly IUserOrderBlModel _userOrderBlModel;

		public UserOrderController(IUserOrderBlModel userOrderBlModel)
		{
			_userOrderBlModel = userOrderBlModel;
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult<UserOrder> GetById(int id)
		{
			var blResult = _userOrderBlModel.GetById(id);

			var result = blResult.Item2;

			return result;
		}


		[HttpPost]
		[AllowAnonymous]
		public ActionResult<UserOrder[]> GetList()
		{
			var blResult = _userOrderBlModel.GetAll();

			var result = blResult.Item2.ToArray();

			return result;
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult<UserOrder[]> GetByStatus(string status)
		{
			var blResult = _userOrderBlModel.GetByStatus(status);

			var result = blResult.Item2;

			return result.ToArray();
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult<UserOrder[]> GetBySearch(string searchString)
		{
			var blResult = _userOrderBlModel.GetBySearch(searchString);

			var result = blResult.Item2;

			return result.ToArray();
		}
		[HttpPost]
		[AllowAnonymous]
		public ActionResult<UserOrder[]> GetByUserId(int userId)
		{
			var blResult = _userOrderBlModel.GetByUserId(userId);

			var result = blResult.Item2;

			return result.ToArray();
		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> SaveReserveOrder(UserOrder userOrder)
		{
			UserOrderValidator validator = new UserOrderValidator();

			ValidationResult validationResult = validator.Validate(userOrder);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _userOrderBlModel.SaveReserveOrder(sessionToken, userOrder);

			return result;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> SaveFinishOrder(UserOrder userOrder)
		{
			UserOrderValidator validator = new UserOrderValidator();

			ValidationResult validationResult = validator.Validate(userOrder);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _userOrderBlModel.SaveFinishOrder(sessionToken, userOrder);

			return result;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Update(UserOrder userOrder)
		{
			UserOrderValidator validator = new UserOrderValidator();

			ValidationResult validationResult = validator.Validate(userOrder);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _userOrderBlModel.UpdateOrder(sessionToken, userOrder);

			return result;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Delete(int id)
		{
			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _userOrderBlModel.DeleteOrder(sessionToken, id);

			return result;
		}



	}
}
