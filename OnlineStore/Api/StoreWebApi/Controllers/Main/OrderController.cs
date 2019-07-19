using System.Linq;
using BLContracts.ActionResults;
using BLContracts.MainService;
using CommonEntities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.FluentValidation;
using BLContracts.Models;
using Microsoft.AspNetCore.Http;

namespace StoreWebApi.Controllers.Main
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IUserOrderService _userOrderService;

		public OrderController(IUserOrderService userOrderService)
		{
			_userOrderService = userOrderService;
		}

		#region GetById

		/// <summary>
		/// Get order  by id
		/// </summary>
		/// <param name="id">id order</param>
		/// <returns>Order</returns>
		/// <response code="200">Returns the order</response>
		/// /// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(UserOrder), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById(int id)
		{

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			var blResult = _userOrderService.GetById(sessionToken,id);

			if (blResult.userOrder == null)
				return StatusCode(StatusCodes.Status404NotFound, "The order does not exist");

			return Ok(blResult.userOrder);
		}

		#endregion

		#region Create

		/// <summary>
		/// Create a order
		/// </summary>
		/// <param name="orderData">Order request</param>
		/// <returns>Order creation result</returns>
		/// <response code="201">Server return results</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Create([FromBody]OrderRequest orderData)
		{
			if (orderData == null)
				return BadRequest("Input request is empty");

			OrderRequestValidator dataRequestValidator = new OrderRequestValidator();

			ValidationResult validationResult = dataRequestValidator.Validate(orderData);

			if (!validationResult.IsValid)
			{
				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _userOrderService.SaveOrder(sessionToken, orderData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion


	}
}
