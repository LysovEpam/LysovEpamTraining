using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLContracts.ActionResults;
using BLContracts.SystemBl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreWebApi.AuthorizationModel;


namespace StoreWebApi.Controllers.SystemContollers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{

		private readonly IAuthorizationBlModel _authorizationBlModel;
		public AuthorizationController(IAuthorizationBlModel authorizationBlModel)
		{
			_authorizationBlModel = authorizationBlModel;
		}


		[HttpPost]
		[AllowAnonymous]
		public BLContracts.ActionResults.System.AuthorizationResult Authorization([FromServices] IJwtSigningEncodingKey signingEncodingKey, string login, string password)
		{

			BaseActionResult blResult = _authorizationBlModel.CheckAuthorization(login, password);

			if (blResult.ResultConnection != BaseActionResult.ResultConnectionEnum.Correct)
			{
				var resultError = new BLContracts.ActionResults.System.AuthorizationResult(blResult, null);
				return resultError;
			}

			string role = _authorizationBlModel.GetUserRole(login, password).GetRoleName();

			var claims = new[]
			{
				new Claim(ClaimTypes.Role, role),
				new Claim(AuthorizationDataModel.ClaimLogin, login),
				new Claim(AuthorizationDataModel.ClaimSessionToken,"SessionKey12345") //for test
			};

			var token = new JwtSecurityToken(
				issuer: AuthorizationDataModel.ValidIssuer,
				audience: AuthorizationDataModel.ValidAudience,
				claims: claims,
				expires: DateTime.Now + AuthorizationDataModel.AuthorizationTtl,
				signingCredentials: new SigningCredentials(
					signingEncodingKey.GetKey(),
					signingEncodingKey.SigningAlgorithm)
			);

			string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			var result = new BLContracts.ActionResults.System.AuthorizationResult(
				BaseActionResult.ResultConnectionEnum.Correct, "", jwtToken);

			return result;


		}


		


	}
}
