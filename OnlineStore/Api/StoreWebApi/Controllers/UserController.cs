using System;
using System.Collections.Generic;
using System.Linq;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.AuthorizationModel;
using StoreWebApi.Logger;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserSystemService _userSystemService;
		private readonly ILoggerManager _logger;

		public UserController(IUserSystemService userSystemService, ILoggerManager logger)
		{
			_userSystemService = userSystemService;
			_logger = logger;
		}

		#region GetAll

		/// <summary>
		/// Get all users 
		/// </summary>
		/// <returns>Users</returns>
		/// <response code="200">Returns all users</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(typeof(IEnumerable<SystemUserData>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetAll()
		{

			var blResult = _userSystemService.GetAll();

			if (blResult.userData == null)
				return StatusCode(StatusCodes.Status404NotFound, "The users does not exist");

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			return Ok(blResult.userData);
		}

		#endregion
		#region GetById

		/// <summary>
		/// Get user by id
		/// </summary>
		/// <returns>User</returns>
		/// <response code="200">Return user</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(typeof(SystemUserData), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetById([FromBody]int id)
		{

			var blResult = _userSystemService.GetById(id);

			if (blResult.userData == null)
				return StatusCode(StatusCodes.Status404NotFound, "The user does not exist");

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			return Ok(blResult.userData);
		}

		#endregion
		#region GetByToken


		/// <summary>
		/// Get user by token
		/// </summary>
		/// <returns>User</returns>
		/// <response code="200">Return user</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(typeof(SystemUserData), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetByToken()
		{
			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			var blResult = _userSystemService.GetByToken(sessionToken);

			if (blResult.userData == null)
				return StatusCode(StatusCodes.Status404NotFound, "The user does not exist");

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {blResult.actionResult.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);
			}

			return Ok(blResult.userData);
		}

		#endregion
		#region Create

		/// <summary>
		/// Create a user
		/// </summary>
		/// <param name="userData">User request</param>
		/// <returns>User creation result</returns>
		/// <response code="201">Server return results</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Create([FromBody]SystemUserData userData)
		{
			if (userData == null)
				return BadRequest("Input request is empty");

			#region Check input data

			try
			{
				var dataRequestValidator = new SystemUserDataValidator();

				var validationResult = dataRequestValidator.Validate(userData);

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
				_logger.LogError($"Create new system user. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}
			#endregion
			#region Check password

			var validatorPassword = new PasswordValidator();
			var validatorPasswordResult = validatorPassword.Validate(userData.NewPassword);

			if (!validatorPasswordResult.IsValid)
			{
				string messageError = validatorPasswordResult.Errors.FirstOrDefault()?.ErrorMessage;
				return Conflict(messageError);
			}

			#endregion

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _userSystemService.Create(sessionToken, userData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {result.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
			}

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion
		#region Update

		/// <summary>
		/// Update a user
		/// </summary>
		/// <param name="userData">User request</param>
		/// <returns>User creation result</returns>
		/// <response code="200">Server return results</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to add</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Update([FromBody]SystemUserData userData)
		{
			if (userData == null)
				return BadRequest("Input request is empty");

			#region Valid data

			try
			{

				var dataRequestValidator = new SystemUserDataValidator();
				var validationResult = dataRequestValidator.Validate(userData);

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
				_logger.LogError($"Update system user. Input data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}
			#endregion
			#region Check password

			var validatorPassword = new PasswordValidator();
			var validatorOldPasswordResult = validatorPassword.Validate(userData.OldPassword);

			if (!validatorOldPasswordResult.IsValid)
			{
				string messageError = validatorOldPasswordResult.Errors.FirstOrDefault()?.ErrorMessage;
				return Conflict(messageError);
			}

			if (!string.IsNullOrEmpty(userData.NewPassword))
			{
				var validatorNewPasswordResult = validatorPassword.Validate(userData.OldPassword);
				if (!validatorNewPasswordResult.IsValid)
				{
					string messageError = validatorNewPasswordResult.Errors.FirstOrDefault()?.ErrorMessage;
					return Conflict(messageError);
				}
			}

			#endregion


			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;


			ServiceResult result = _userSystemService.Update(sessionToken, userData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {result.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
			}

			return StatusCode(StatusCodes.Status200OK);

		}

		#endregion
		#region Delete

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="id">Id user</param>        
		/// <returns>User delete result</returns>
		/// <response code="200">Delete success</response>
		/// <response code="400">If the item is null</response>
		/// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpDelete("{id}")]
		[Authorize(Roles = AuthorizationDataModel.RoleAdmin)]
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


			ServiceResult result = _userSystemService.Delete(sessionToken, id);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
			{
				_logger.LogError($"User system service error: {result.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
			}

			return Ok();

		}

		#endregion

	}
}
