using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Models.ControllerResults;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationService _registrationService;
		public RegistrationController(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		/// <summary>
		/// Registration new system user
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /RegistrationData
		///     {
		///			"FirstName" : "TestFirstName",
		///			"LastName" : "TestLastName",
		///			"Email" : "TestEmail@email.net",
		///			"Phone" : "PhoneTestNumber", 
		///			"Login": "TestLoginUser",
		///			"Password": "Password"
		///     }
		///
		/// </remarks>
		/// <param name="registrationData">registration data</param>
		/// <response code="200">Registration completed successfully</response>
		/// <response code="400">If the request is null</response>
		/// <response code="409">If input error</response>
		/// <response code="500">If a server error occurred while processing the request</response>
		[HttpPost]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(RegistrationErrors), StatusCodes.Status409Conflict)]
		[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
		public ActionResult RegistrationNewUser([FromBody]RegistrationRequest registrationData)
		{

			if (registrationData == null)
				return BadRequest("Empty request");
			
			var validator = new RegistrationRequestValidator();
			var validationResult = validator.Validate(registrationData);

			if (!validationResult.IsValid)
			{
				var registrationErrors = new RegistrationErrors();

				foreach (var error in validationResult.Errors)
				{
					if (error.PropertyName == nameof(RegistrationRequest.FirstName))
						registrationErrors.FirstNameError = error.ErrorMessage;
					if (error.PropertyName == nameof(RegistrationRequest.LastName))
						registrationErrors.LastNameError = error.ErrorMessage;
					if (error.PropertyName == nameof(RegistrationRequest.Email))
						registrationErrors.EmailError = error.ErrorMessage;
					if (error.PropertyName == nameof(RegistrationRequest.Phone))
						registrationErrors.PhoneError = error.ErrorMessage;
					if (error.PropertyName == nameof(RegistrationRequest.Login))
						registrationErrors.LoginError = error.ErrorMessage;
					if (error.PropertyName == nameof(RegistrationRequest.Password))
						registrationErrors.PasswordError = error.ErrorMessage;
				}

				return Conflict(registrationErrors);
			}

			
			var registrationResult = _registrationService.CreateNewUser(registrationData);

			if (registrationResult.ResultConnection == ServiceResult.ResultConnectionEnum.Correct)
				return Ok();

			return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error please try again later");



		}



	}
}
