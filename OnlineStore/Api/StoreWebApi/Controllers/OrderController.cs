using System;
using System.Collections.Generic;
using System.Linq;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.Logger;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IUserOrderService _userOrderService;
		private readonly ILoggerManager _logger;

		public OrderController(IUserOrderService userOrderService, ILoggerManager logger)
		{
			_userOrderService = userOrderService;
			_logger = logger;
		}

		#region GetById

		/// <summary>
		/// Get order by id
		/// </summary>
		/// <param name="id">id order</param>
		/// <returns>Order</returns>
		/// <response code="200">Returns the order</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(typeof(UserOrder), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById([FromBody]int id)
		{
			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;


			var blResult = _userOrderService.GetById(id, sessionToken);

			if (blResult.userOrder == null)
			{
				_logger.LogError($"Order information service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			return Ok(blResult.userOrder);
		}

		#endregion
		#region GetAll

		/// <summary>
		/// Get all orders 
		/// </summary>
		/// <returns>Order</returns>
		/// <response code="200">Returns the order</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(typeof(IEnumerable<OrderData>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetAll()
		{

			var blResult = _userOrderService.GetAll();

			if (blResult.userOrders == null)
			{
				_logger.LogError($"Order information service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			return Ok(blResult.userOrders);
		}


		#endregion
		#region GetByUser

		/// <summary>
		/// Get user orders 
		/// </summary>
		/// <returns>Orders</returns>
		/// <response code="200">Returns the order</response>
		/// <response code="400">Bad request</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(typeof(IEnumerable<OrderData>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetByUser()
		{

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;


			var blResult = _userOrderService.GetByUser(sessionToken);

			if (blResult.actionResult.ResultConnection == ServiceResult.ResultConnectionEnum.Correct &&
				blResult.userOrders == null)
				return NotFound("Orders not exist");

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"Order information service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}
			return Ok(blResult.userOrders);
		}

		#endregion
		#region GetBySearch

		/// <summary>
		/// Get search orders 
		/// </summary>
		/// <returns>Orders</returns>
		/// <response code="200">Returns the order</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(typeof(IEnumerable<OrderData>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetBySearch([FromBody]OrderSearchRequest searchRequest)
		{
			if (searchRequest == null)
				return StatusCode(StatusCodes.Status400BadRequest, "Search request cannot be empty");

			try
			{

				var dataRequestValidator = new OrderSearchRequestValidator();
				var validationResult = dataRequestValidator.Validate(searchRequest);

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
				_logger.LogError($"Search orders. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}

			var blResult = _userOrderService.GetBySearch(searchRequest);

			if (blResult.actionResult.ResultConnection == ServiceResult.ResultConnectionEnum.Correct &&
				blResult.userOrders == null)
				return NotFound("Orders not exist");

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			return Ok(blResult.userOrders);
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

			#region validate data

			try
			{
				var dataRequestValidator = new OrderRequestValidator();

				var validationResult = dataRequestValidator.Validate(orderData);

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
				_logger.LogError($"Create new orders. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}

			#endregion




			ServiceResult result = _userOrderService.SaveOrder(orderData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion
		#region UpdateOrder

		/// <summary>
		/// Update a order
		/// </summary>
		/// <param name="orderData">Order request</param>
		/// <returns>Order update result</returns>
		/// <response code="200">Server return results</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult UpdateOrder([FromBody]OrderRequest orderData)
		{
			if (orderData == null)
				return BadRequest("Input request is empty");

			#region validate data

			try
			{

				var dataRequestValidator = new OrderRequestValidator();

				var validationResult = dataRequestValidator.Validate(orderData);

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
				_logger.LogError($"Update orders. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}

			#endregion

			ServiceResult result = _userOrderService.UpdateOrder(orderData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion
		#region DeleteOrder

		/// <summary>
		/// Delete a order
		/// </summary>
		/// <param name="id">Idorder</param>        
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
		public ActionResult DeleteOrder(int id)
		{
			if (id < 1)
				return BadRequest("Input request is empty");


			ServiceResult result = _userOrderService.DeleteOrder(id);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return Ok();
		}

		#endregion

	}
}
