//using System.Linq;
//using BL.OnlineStore.FluentValidation;
//using BLContracts.ActionResults;
//using BLContracts.MainService;
//using CommonEntities;
//using CommonEntities.Additional;
//using FluentValidation.Results;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using StoreWebApi.AuthorizationModel;

//namespace StoreWebApi.Controllers.Main
//{
//	[Route("api/[controller]/[action]")]
//	[ApiController]
//	public class UserOrderController : Controller
//	{
//		private readonly IUserOrderService _userOrderService;

//		public UserOrderController(IUserOrderService userOrderService)
//		{
//			_userOrderService = userOrderService;
//		}

//		[HttpPost]
//		[AllowAnonymous]
//		public ActionResult<UserOrder> GetById(int id)
//		{
//			var blResult = _userOrderService.GetById(id);

//			var result = blResult.Item2;

//			return result;
//		}


//		[HttpPost]
//		[AllowAnonymous]
//		public ActionResult<UserOrder[]> GetList()
//		{
//			var blResult = _userOrderService.GetAll();

//			var result = blResult.Item2.ToArray();

//			return result;
//		}

//		[HttpPost]
//		[AllowAnonymous]
//		public ActionResult<UserOrder[]> GetByStatus(string status)
//		{
//			var blResult = _userOrderService.GetByStatus(status);

//			var result = blResult.Item2;

//			return result.ToArray();
//		}

//		[HttpPost]
//		[AllowAnonymous]
//		public ActionResult<UserOrder[]> GetBySearch(string searchString)
//		{
//			var blResult = _userOrderService.GetBySearch(searchString);

//			var result = blResult.Item2;

//			return result.ToArray();
//		}
//		[HttpPost]
//		[AllowAnonymous]
//		public ActionResult<UserOrder[]> GetByUserId(int userId)
//		{
//			var blResult = _userOrderService.GetByUserId(userId);

//			var result = blResult.Item2;

//			return result.ToArray();
//		}

//		[HttpPost]
//		[Authorize]
//		[Authorize(Roles = UserRole.RoleEditor)]
//		[Authorize(Roles = UserRole.RoleAdmin)]
//		public ActionResult<BaseActionResult> SaveReserveOrder(UserOrder userOrder)
//		{
//			UserOrderValidator validator = new UserOrderValidator();

//			ValidationResult validationResult = validator.Validate(userOrder);

//			if (!validationResult.IsValid)
//			{
//				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
//					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

//				return resultError;
//			}

//			var claims = HttpContext.User.Claims.ToList();
//			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

//			BaseActionResult result = _userOrderService.SaveReserveOrder(sessionToken, userOrder);

//			return result;

//		}

//		[HttpPost]
//		[Authorize]
//		[Authorize(Roles = UserRole.RoleEditor)]
//		[Authorize(Roles = UserRole.RoleAdmin)]
//		public ActionResult<BaseActionResult> SaveFinishOrder(UserOrder userOrder)
//		{
//			UserOrderValidator validator = new UserOrderValidator();

//			ValidationResult validationResult = validator.Validate(userOrder);

//			if (!validationResult.IsValid)
//			{
//				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
//					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

//				return resultError;
//			}

//			var claims = HttpContext.User.Claims.ToList();
//			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

//			BaseActionResult result = _userOrderService.SaveFinishOrder(sessionToken, userOrder);

//			return result;

//		}

//		[HttpPost]
//		[Authorize]
//		[Authorize(Roles = UserRole.RoleEditor)]
//		[Authorize(Roles = UserRole.RoleAdmin)]
//		public ActionResult<BaseActionResult> Update(UserOrder userOrder)
//		{
//			UserOrderValidator validator = new UserOrderValidator();

//			ValidationResult validationResult = validator.Validate(userOrder);

//			if (!validationResult.IsValid)
//			{
//				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
//					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

//				return resultError;
//			}

//			var claims = HttpContext.User.Claims.ToList();
//			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

//			BaseActionResult result = _userOrderService.UpdateOrder(sessionToken, userOrder);

//			return result;

//		}

//		[HttpPost]
//		[Authorize]
//		[Authorize(Roles = UserRole.RoleEditor)]
//		[Authorize(Roles = UserRole.RoleAdmin)]
//		public ActionResult<BaseActionResult> Delete(int id)
//		{
//			var claims = HttpContext.User.Claims.ToList();
//			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

//			BaseActionResult result = _userOrderService.DeleteOrder(sessionToken, id);

//			return result;
//		}



//	}
//}
