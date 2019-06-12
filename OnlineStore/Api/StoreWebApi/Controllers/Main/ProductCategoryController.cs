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
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryBlModel _productCategoryBlModel;

		public ProductCategoryController(IProductCategoryBlModel categoryBlModel)
		{
			_productCategoryBlModel = categoryBlModel;
		}

		#region GetList

		/// <summary>
		/// Get list all product categories
		/// </summary>
		/// <returns>A newly created product category</returns>
		/// <response code="200">Returns list all product categories</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		public ActionResult<ProductCategory[]> GetList()
		{
			var blResult = _productCategoryBlModel.GetAll();

			var result = blResult.Item2.ToArray();

			return result;
		}

		#endregion
		#region GetById

		/// <summary>
		/// Get product category by id
		/// </summary>
		/// <param name="id">id product category</param>
		/// <returns>product category</returns>
		/// <response code="200">Returns the product category</response>
		/// <response code="400">If the id is null</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public ActionResult<ProductCategory> GetById(int id)
		{
			var blResult = _productCategoryBlModel.GetById(id);

			var result = blResult.Item2;

			return result;
		}
		#endregion
		#region GetByName

		/// <summary>
		/// Get product category by id
		/// </summary>
		/// <param name="categoryName">name product category</param>
		/// <returns>product categories</returns>
		/// <response code="200">Returns the product categories</response>
		/// <response code="400">If the category name is null</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public ActionResult<ProductCategory[]> GetByName(string categoryName)
		{
			var blResult = _productCategoryBlModel.GetByName(categoryName);

			var result = blResult.Item2.ToArray();

			return result;
		}

		#endregion
		#region SearchCategory

		/// <summary>
		/// Search product category
		/// </summary>
		/// <param name="searchString">search string</param>
		/// <returns>product categories</returns>
		/// <response code="200">Returns the product category</response>
		/// <response code="400">If the search string is null</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public ActionResult<ProductCategory[]> SearchCategory(string searchString)
		{
			var blResult = _productCategoryBlModel.SearchCategory(searchString);

			var result = blResult.Item2.ToArray();

			return result;
		}

		#endregion
		#region Create

		/// <summary>
		/// Create a product category
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /ProductCategory
		///     {
		///        "IdEntity": null,
		///        "CategoryName": "New category name",
		///        "Description": "New description"
		///     }
		///
		/// </remarks>
		/// <param name="productCategory"></param>
		/// <returns>Category creation result</returns>
		/// <response code="201">The result of creating a new category</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response> 
		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public ActionResult<BaseActionResult> Create(ProductCategory productCategory)
		{
			ProductCategoryValidator validator = new ProductCategoryValidator();

			ValidationResult validationResult = validator.Validate(productCategory);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _productCategoryBlModel.SaveNewCategory(sessionToken, productCategory);

			return result;

		}

		#endregion
		#region Update

		/// <summary>
		/// Update a product category
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /ProductCategory
		///     {
		///        "IdEntity": 1,
		///        "CategoryName": "category update",
		///        "Description": "description update"
		///     }
		///
		/// </remarks>
		/// <param name="productCategory"></param>
		/// <returns>Category update result</returns>
		/// <response code="200">Update success</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to update</response> 
		[HttpPost]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public ActionResult<BaseActionResult> Update(ProductCategory productCategory)
		{
			ProductCategoryValidator validator = new ProductCategoryValidator();

			ValidationResult validationResult = validator.Validate(productCategory);

			if (!validationResult.IsValid)
			{
				BaseActionResult resultError = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
					"Модель не прошла валидацию");  //ЗАМЕНИТЬ! Добавить отправку сообщения об ошибке

				return resultError;
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _productCategoryBlModel.UpdateCategory(sessionToken, productCategory);

			return result;

		}

		#endregion
		#region Delete

		/// <summary>
		/// Delete a specific product category
		/// </summary>
		/// <param name="id">Id product category</param>        
		/// <returns>Category delete result</returns>
		/// <response code="200">Delete success</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response> 
		[HttpDelete("{id}")]
		[Authorize]
		[Authorize(Roles = UserRole.RoleEditor)]
		[Authorize(Roles = UserRole.RoleAdmin)]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public ActionResult<BaseActionResult> Delete(int id)
		{
			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			BaseActionResult result = _productCategoryBlModel.DeleteCategory(sessionToken, id);

			return result;
		}

		#endregion








	}
}