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
	public class ProductInformationController : Controller
	{
		private readonly IProductInformationBlModel _productInformationBlModel;
		private readonly IImageProductBlModel _imageBlModel;

		public ProductInformationController(IProductInformationBlModel productInformationBlModel, IImageProductBlModel imageBlModel)
		{
			_productInformationBlModel = productInformationBlModel;
			_imageBlModel = imageBlModel;

		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<ProductInformation[]> GetList()
		{
			var blResult = _productInformationBlModel.GetAll();

			if (blResult.Item1.ResultConnection == BaseActionResult.ResultConnectionEnum.Correct)
			{
				if (blResult.Item2 != null)
				{
					foreach (var productList in blResult.Item2)
					{
						string imageFullSource = _imageBlModel.GetImageFullSource(productList.ImageLocalSource);
						productList.ImageLocalSource = imageFullSource;
					}

					var result = blResult.Item2.ToArray();

					return result;
				}
			}

			return null;


		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult<ProductInformation> GetById(int id)
		{
			var blResult = _productInformationBlModel.GetById(id);

			if (blResult.Item1.ResultConnection == BaseActionResult.ResultConnectionEnum.Correct)
			{
				if (blResult.Item2 != null)
				{
					string imageFullSource = _imageBlModel.GetImageFullSource(blResult.Item2.ImageLocalSource);
					blResult.Item2.ImageLocalSource = imageFullSource;

					return blResult.Item2;
				}
			}

			return null;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Create(ProductInformation productInformation)
		{
			ProductInformationValidator validator = new ProductInformationValidator();

			ValidationResult validationResult = validator.Validate(productInformation);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;


			string smallPath = _imageBlModel.GetSmallSource(productInformation.ImageLocalSource);
			productInformation.ImageLocalSource = smallPath;


			BaseActionResult result = _productInformationBlModel.SaveNewProductInformation(sessionToken, productInformation);

			return result;

		}

		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		public ActionResult<BaseActionResult> Update(ProductInformation productInformation)
		{
			ProductInformationValidator validator = new ProductInformationValidator();

			ValidationResult validationResult = validator.Validate(productInformation);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			string smallPath = _imageBlModel.GetSmallSource(productInformation.ImageLocalSource);
			productInformation.ImageLocalSource = smallPath;

			BaseActionResult result = _productInformationBlModel.UpdateProductInformation(sessionToken, productInformation);

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

			BaseActionResult result = _productInformationBlModel.DeleteProductInformation(sessionToken, id);

			return result;
		}

	}
}
