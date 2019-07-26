using System;
using System.Collections.Generic;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Services;
using CommonEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.Logger;


namespace StoreWebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryService _productCategoryService;
		private readonly ILoggerManager _logger;


		public ProductCategoryController(IProductCategoryService categoryService, ILoggerManager logger)
		{
			_productCategoryService = categoryService;
			_logger = logger;
		}


		#region GetById

		/// <summary>
		/// Get product category by id
		/// </summary>
		/// <param name="id">id product category</param>
		/// <returns>Product category</returns>
		/// <response code="200">Returns the product category</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet("{id}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById(int id)
		{
			var blResult = _productCategoryService.GetById(id);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"Product category service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			if (blResult.productCategory == null)
				return StatusCode(StatusCodes.Status404NotFound, "The product category does not exist");

			return Ok(blResult.productCategory);
		}
		#endregion
		#region GetList

		/// <summary>
		/// Get list all product categories
		/// </summary>
		/// <returns>Product categories</returns>
		/// <response code="200">Returns list all product categories</response>
		/// <response code="404">If entities do not exist</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<ProductCategory>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetList()
		{

			var blResult = _productCategoryService.GetAll();

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"Product category service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			if (blResult.productCategories != null && blResult.productCategories.Count == 0)
				return StatusCode(StatusCodes.Status404NotFound, "Product categories do not exist");

			return Ok(blResult.productCategories);

		}

		#endregion
		#region Search

		/// <summary>
		/// Search product category
		/// </summary>
		/// <param name="searchString">search string</param>
		/// <returns>product categories</returns>
		/// <response code="200">Returns the product category</response>
		/// <response code="400">If the search string is null</response>
		/// <response code="404">If the category does not exist</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet("{searchString}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<ProductCategory>), 200)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return StatusCode(StatusCodes.Status400BadRequest, "Search string cannot be empty");

			var blResult = _productCategoryService.SearchCategory(searchString);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"Product category service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			if (blResult.productCategories != null && blResult.productCategories.Count == 0)
				return StatusCode(StatusCodes.Status404NotFound, "Product categories do not exist");


			return Ok(blResult.productCategories);
		}

		#endregion
		#region Create

		/// <summary>
		/// Create a product category
		/// </summary>
		/// <param name="productCategory">Product category</param>
		/// <returns>Category creation result</returns>
		/// <response code="201">Server return results</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleEditorAndAdmin)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Create([FromBody]ProductCategory productCategory)
		{
			if (productCategory == null)
				return BadRequest("Input request is empty");

			try
			{

				var validator = new ProductCategoryValidator();

				var validationResult = validator.Validate(productCategory);

				if (!validationResult.IsValid)
				{
					string errorMessage = "";

					foreach (var error in validationResult.Errors)
						errorMessage += error.ErrorMessage + " ";

					return Conflict(errorMessage);
				}

			}
			catch (Exception e)
			{
				_logger.LogError($"Create new category failed. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}


			ServiceResult result = _productCategoryService.SaveNewCategory(productCategory);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return CreatedAtAction(nameof(GetById), new { id = productCategory.IdEntity }, productCategory);


		}

		#endregion
		#region Update

		/// <summary>
		/// Update a product category
		/// </summary>
		/// <param name="productCategory"></param>
		/// <returns>Category update result</returns>
		/// <response code="200">Update success</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to update</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleEditorAndAdmin)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Update([FromBody]ProductCategory productCategory)
		{
			if (productCategory == null)
				return BadRequest("Input request is empty");

			try
			{ 
				var validator = new ProductCategoryValidator();

				var validationResult = validator.Validate(productCategory);

				if (!validationResult.IsValid)
				{
					string errorMessage = "";

					foreach (var error in validationResult.Errors)
						errorMessage += error.ErrorMessage + " ";

					return Conflict(errorMessage);
				}

			}
			catch (Exception e)
			{
				_logger.LogError($"Update category failed. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}


			ServiceResult result = _productCategoryService.UpdateCategory(productCategory);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();

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
		/// <response code="403">If the user does not have access</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpDelete("{id}")]
		[Authorize(Roles = AuthorizationDataModel.RoleEditorAndAdmin)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Delete(int id)
		{
			if (id < 1)
				return BadRequest("Input request is empty");


			ServiceResult result = _productCategoryService.DeleteCategory(id);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"Delete category failed. Service error message: {result.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
			}

			return Ok();
		}

		#endregion

	}
}