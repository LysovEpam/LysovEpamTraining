using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreWebApi.AuthorizationModel;
using AuthorizationResult = StoreWebApi.Models.ControllerResults.AuthorizationResult;
using IAuthorizationService = BLContracts.Services.IAuthorizationService;


namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{

		private readonly IAuthorizationService _authorizationService;
		public AuthorizationController(IAuthorizationService authorizationService)
		{
			_authorizationService = authorizationService;
		}


		///  <summary>
		///  User authorization
		///  </summary>
		///  <remarks>
		///  Sample request:
		/// 
		///      POST /AuthorizationRequest
		///      {
		/// 		"Login": "user",
		/// 		"Password": "12345678"
		///      }
		/// 
		///  </remarks>
		///  <param name="signingEncodingKey"></param>
		///  <param name="authorizationRequest">Authorization data request</param>
		///  <response code="200">Authorization completed successfully</response>
		///  <response code="400">If the request is null</response>
		///  <response code="409">If input error</response>
		///  <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[AllowAnonymous]
		[ProducesResponseType(typeof(AuthorizationResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult Authorization([FromServices] IJwtSigningEncodingKey signingEncodingKey, [FromBody]AuthorizationRequest authorizationRequest)
		{
			#region Valid input data

			if (authorizationRequest == null)
				return BadRequest("Empty request");

			var validator = new AuthorizationRequestValidator();
			var validationResult = validator.Validate(authorizationRequest);

			if (!validationResult.IsValid)
				return Conflict("Invalid input data request");

			#endregion
			#region Check authorization

			var blResult = _authorizationService.CheckAuthorization(authorizationRequest);

			if (blResult.ResultConnection != ServiceResult.ResultConnectionEnum.Correct)
				return Conflict(blResult.Message);

			#endregion

			var authorizationResult =_authorizationService.AuthorizationUser(authorizationRequest);

			if (authorizationResult.sessionToken == null || authorizationResult.userRole == null)
				return StatusCode(StatusCodes.Status500InternalServerError, "Failed to authenticate user, try again");


			var claims = new[]
			{
				new Claim(ClaimTypes.Role, authorizationResult.userRole.GetRoleName()),
				new Claim(AuthorizationDataModel.ClaimLogin, authorizationRequest.Login),
				new Claim(AuthorizationDataModel.ClaimSessionToken,authorizationResult.sessionToken)
			};

			var token = new JwtSecurityToken(
				AuthorizationDataModel.ValidIssuer,
				AuthorizationDataModel.ValidAudience,
				claims,
				expires: authorizationResult.authorizationFinish,
				signingCredentials: new SigningCredentials(
					signingEncodingKey.GetKey(),
					signingEncodingKey.SigningAlgorithm)
			);

			string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			var result = new AuthorizationResult(authorizationRequest.Login, authorizationResult.userRole, jwtToken,
				authorizationResult.authorizationFinish);

			
			return Ok(result);


		}





	}
}
