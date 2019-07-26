using System;
using System.Linq;
using BL.OnlineStore.FluentValidation;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Logger;
using StoreWebApi.Models.ControllerResults;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationService _registrationService;
		private readonly ILoggerManager _logger;

		public RegistrationController(IRegistrationService registrationService, ILoggerManager logger)
		{
			_registrationService = registrationService;
			_logger = logger;
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

			try
			{
				var validator = new RegistrationRequestValidator();
				var validationResult = validator.Validate(registrationData);
				var validatorPassword = new PasswordValidator();
				var validatorPasswordResult = validatorPassword.Validate(registrationData.Password);

				var registrationErrors = new RegistrationErrors();

				if (!validationResult.IsValid)
				{
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
					}

					return Conflict(registrationErrors);
				}

				if (!validatorPasswordResult.IsValid)
				{
					registrationErrors.PasswordError = validatorPasswordResult.Errors.FirstOrDefault()?.ErrorMessage;
				}
			}
			catch (Exception e)
			{
				_logger.LogError($"Registration failed. User data failed validation. Full validator exception message: {e.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
			}



			_logger.LogInfo($"New user registration( login:'{registrationData.Login}')");

			var registrationResult = _registrationService.CreateNewUser(registrationData);

			if (registrationResult.ResultConnection == ServiceResult.ResultConnectionEnum.Correct)
				return Ok();

			_logger.LogError($"Error registration new user, internal server error");

			return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error please try again later");



		}



	}
}
