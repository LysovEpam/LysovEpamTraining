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
	public class UserController : Controller
	{
		private readonly IUserSystemService _userSystemService;

		public UserController(IUserSystemService userSystemService)
		{
			_userSystemService = userSystemService;
		}

		#region GetByMyself

		/// <summary>
		/// Get user by token
		/// </summary>
		/// <returns>User</returns>
		/// <response code="200">Returns the order</response>
		/// /// <response code="401">If the user is not authorized or there is no permission to delete</response>
		/// <response code="403">If the user does not have access</response>
		/// <response code="404">If the entity does not exist</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[Authorize(Roles = AuthorizationDataModel.RoleAllRoles)]
		[ProducesResponseType(typeof(UserSystem), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult GetByMyself()
		{
			
			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;
			string login = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimLogin)?.Value;


			var blResult = _userSystemService.GetUserInformation(sessionToken, login);

			if (blResult.actionResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, blResult.actionResult.Message);

			return Ok(blResult.systemUserData);
		}

		#endregion
		#region UpdateByMyself

		/// <summary>
		/// Update user
		/// </summary>
		/// <param name="userData">User request</param>
		/// <returns>User update result</returns>
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
		public ActionResult UpdateByMyself([FromBody]SystemUserData userData)
		{
			if (userData == null)
				return BadRequest("Input request is empty");

			SystemUserDataValidator dataRequestValidator = new SystemUserDataValidator();

			ValidationResult validationResult = dataRequestValidator.Validate(userData);

			if (!validationResult.IsValid)
			{
				string errorMessage = "";

				foreach (var error in validationResult.Errors)
					errorMessage += error.ErrorMessage + " ";

				return Conflict(errorMessage);
			}

			var claims = HttpContext.User.Claims.ToList();
			string sessionToken = claims.FirstOrDefault(c => c.Type == AuthorizationDataModel.ClaimSessionToken)?.Value;

			ServiceResult result = _userSystemService.UpdateUserByMyself(sessionToken, userData);

			if (result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

			return StatusCode(StatusCodes.Status201Created);

		}

		#endregion
	}
}
