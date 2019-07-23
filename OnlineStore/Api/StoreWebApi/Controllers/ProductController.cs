using System.Collections.Generic;
using System.Linq;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		#region GetById

		/// <summary>
		/// Get product  by id
		/// </summary>
		/// <param name="id">id product</param>
		/// <returns>Product</returns>
		/// <response code="200">Returns the product</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet("{id}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById(int id)
		{
			var blResult = _productService.GetById(id);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.product == null)
				return StatusCode(StatusCodes.Status404NotFound, "The product does not exist");

			return Ok(blResult.product);

		}

		#endregion
		#region GetList

		/// <summary>
		/// Get list all products
		/// </summary>
		/// <returns>Products</returns>
		/// <response code="200">Returns list all products</response>
		/// <response code="404">If entities do not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetList()
		{
			var blResult = _productService.GetAll();

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.products != null && !blResult.products.Any())
				return StatusCode(StatusCodes.Status404NotFound, "Product do not exist");

			return Ok(blResult.products);

		}

		#endregion
		#region Search

		/// <summary>
		/// Search product
		/// </summary>
		/// <param name="searchRequest">search request</param>
		/// <returns>products</returns>
		/// <response code="200">Returns the products</response>
		/// <response code="400">If the search string is null</response>
		/// <response code="404">If the products does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<Product>), 200)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Search(ProductSearchRequest searchRequest)
		{
			if (searchRequest == null)
				return StatusCode(StatusCodes.Status400BadRequest, "Search request cannot be empty");

			var blResult = _productService.Search(searchRequest);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.products != null && !blResult.products.Any())
				return StatusCode(StatusCodes.Status404NotFound, "Product do not exist");

			return Ok(blResult.products);

		}

		#endregion
		#region GetByIdList

		/// <summary>
		/// Search product
		/// </summary>
		/// <param name="idProducts">search request</param>
		/// <returns>products</returns>
		/// <response code="200">Returns the products</response>
		/// <response code="400">If the search string is null</response>
		/// <response code="404">If the products does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<Product>), 200)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetByIdList(IEnumerable<int> idProducts)
		{
			if (idProducts == null)
				return StatusCode(StatusCodes.Status400BadRequest, "Product id cannot be empty");

			var blResult = _productService.GetByIdList(idProducts);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.products == null)
				return StatusCode(StatusCodes.Status404NotFound, "Product do not exist");


			return Ok(blResult.products);
		}

		#endregion

		
		#region Create

		/// <summary>
		/// Create a product
		/// </summary>
		/// <param name="productData">Product</param>
		/// <returns>Product creation result</returns>
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
		public ActionResult Create([FromBody]ProductDataRequest productData)
		{
			if (productData == null)
				return BadRequest("Input request is empty");

			var dataRequestValidator = new ProductDataRequestValidator();

			var validationResult = dataRequestValidator.Validate(productData);

			if (!validationResult.IsValid)
			{
				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}
			
			
			ServiceResult result = _productService.SaveNewProduct(productData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion
		#region Update

		/// <summary>
		/// Update a product
		/// </summary>
		/// <param name="productData">Product data request</param>
		/// <returns>Product update result</returns>
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
		public ActionResult Update([FromBody]ProductDataRequest productData)
		{
			if (productData == null)
				return BadRequest("Input request is empty");


			var dataRequestValidator = new ProductDataRequestValidator();

			var validationResult = dataRequestValidator.Validate(productData);


			if (!validationResult.IsValid)
			{

				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}


			ServiceResult result = _productService.UpdateProduct(productData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();

		}

		#endregion
		#region Delete

		/// <summary>
		/// Delete a specific product
		/// </summary>
		/// <param name="id">Id product</param>        
		/// <returns>Product delete result</returns>
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

			
			ServiceResult result = _productService.DeleteProduct(id);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();

		}

		#endregion
	}
}
