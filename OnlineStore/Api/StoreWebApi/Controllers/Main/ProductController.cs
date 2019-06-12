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
	public class ProductController : Controller
	{
		private readonly IProductBlModel _productBlModel;

		public ProductController(IProductBlModel productBlModel)
		{
			_productBlModel = productBlModel;
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<Product[]> GetList()
		{
			var blResult = _productBlModel.GetAll();

			var result = blResult.Item2.ToArray();

			return result;
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<Product> GetById(int id)
		{
			var blResult = _productBlModel.GetById(id);

			var result = blResult.Item2;

			return result;
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<Product[]> GetByStatus(string status)
		{
			var blResult = _productBlModel.GetByStatus(status);

			var result = blResult.Item2.ToArray();

			return result;
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<Product[]> SearchCategory(string searchString)
		{
			var blResult = _productBlModel.GetBySearch(searchString);

			var result = blResult.Item2.ToArray();

			return result;
		}


		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Create(Product product)
		{
			ProductValidator validator = new ProductValidator();

			ValidationResult validationResult = validator.Validate(product);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _productBlModel.SaveNewProduct(sessionToken, product);

			return result;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Update(Product product)
		{
			ProductValidator validator = new ProductValidator();

			ValidationResult validationResult = validator.Validate(product);


			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _productBlModel.UpdateProduct(sessionToken, product);

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

			BaseActionResult result = _productBlModel.DeleteProduct(sessionToken, id);

			return result;
		}
	}
}
