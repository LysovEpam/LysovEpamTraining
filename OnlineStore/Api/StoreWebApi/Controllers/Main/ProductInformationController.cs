using System.Collections.Generic;
using System.Linq;
using BLContracts.MainService;
using CommonEntities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.FluentValidation;
using CommonEntities.Additional;
using BLContracts.ActionResults;

namespace StoreWebApi.Controllers.Main
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductInformationController : Controller
	{
		private readonly IProductInformationService _productInformationService;
		

		public ProductInformationController(IProductInformationService productInformationService)
		{
			_productInformationService = productInformationService;
			

		}

		#region GetById

		/// <summary>
		/// Get product information by id
		/// </summary>
		/// <param name="id">id product information</param>
		/// <returns>product information</returns>
		/// <response code="200">Returns the product information</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet("{id}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(ProductInformation), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById(int id)
		{

			var blResult = _productInformationService.GetById(id);

			if (blResult.actionResult.ResultConnection !=  ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.prouductInformation == null)
				return StatusCode(StatusCodes.Status404NotFound, "The product information does not exist");

			return Ok(blResult.prouductInformation);

		}

		#endregion
		#region GetList

		/// <summary>
		/// Get list all product informations
		/// </summary>
		/// <returns>Product informations</returns>
		/// <response code="200">Returns list all product informations</response>
		/// <response code="404">If entities do not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<ProductInformation>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetList()
		{ 
			var blResult = _productInformationService.GetAll();

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.productInformations != null && blResult.productInformations.Count == 0)
				return StatusCode(StatusCodes.Status404NotFound, "Product informations do not exist");

			return Ok(blResult.productInformations);
		}

		#endregion
		#region Search

		/// <summary>
		/// Search product information
		/// </summary>
		/// <param name="searchString">search string</param>
		/// <returns>product informations</returns>
		/// <response code="200">Returns the product informations</response>
		/// <response code="400">If the search string is null</response>
		/// <response code="404">If the category does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpGet("{searchString}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<ProductInformation>), 200)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return StatusCode(StatusCodes.Status400BadRequest, "Search string cannot be empty");

			var blResult = _productInformationService.SearchInformation(searchString);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			if (blResult.productInformations != null && blResult.productInformations.Count == 0)
				return StatusCode(StatusCodes.Status404NotFound, "Product categories do not exist");


			return Ok(blResult.productInformations);
		}

		#endregion
		#region Create

		/// <summary>
		/// Create a product information
		/// </summary>
		/// <param name="productInformation">Product information</param>
		/// <returns>Product information creation result</returns>
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
		public ActionResult Create([FromBody]ProductInformation productInformation)
		{
			if (productInformation == null)
				return BadRequest("Input request is empty");

			ProductInformationValidator validator = new ProductInformationValidator();

			ValidationResult validationResult = validator.Validate(productInformation);

			if (!validationResult.IsValid)
			{
				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _productInformationService.SaveNewProductInformation(sessionToken, productInformation);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return CreatedAtAction(nameof(GetById), new { id = productInformation.IdEntity }, productInformation);



		}

		#endregion
		#region Update

		/// <summary>
		/// Update a product information
		/// </summary>
		/// <param name="productInformation"></param>
		/// <returns>Product information update result</returns>
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
		public ActionResult Update([FromBody]ProductInformation productInformation)
		{
			if (productInformation == null)
				return BadRequest("Input request is empty");

			ProductInformationValidator validator = new ProductInformationValidator();
			ValidationResult validationResult = validator.Validate(productInformation);

			if (!validationResult.IsValid)
			{
				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _productInformationService.UpdateProductInformation(sessionToken, productInformation);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();

		}

		#endregion
		#region Delete

		/// <summary>
		/// Delete a specific product information
		/// </summary>
		/// <param name="id">Id product information</param>        
		/// <returns>product information delete result</returns>
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

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _productInformationService.DeleteProductInformation(sessionToken, id);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();

		}

		#endregion

	}
}
