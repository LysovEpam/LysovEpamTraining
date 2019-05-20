using BLContracts.ActionResults;
using BLContracts.ActionResults.System;
using BLContracts.SystemBl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreWebApi.Model.ApiJsonResult;
using StoreWebApi.Model.ApiJsonResult.JsonData;


namespace StoreWebApi.Controllers.SystemContollers
{

	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationBlModel _registrationBlModel;
		public RegistrationController(IRegistrationBlModel registrationBlModel)
		{
			_registrationBlModel = registrationBlModel;
		}

		[HttpPost]
		public string RegistrationNewUser(string firstName, string lastName, string email, string phone, string login, string password)
		{

			RegistrationResult registrationResult = _registrationBlModel.CreateNewUser(firstName, lastName, email, phone, login, password);


			if (registrationResult.ResultConnection != BaseActionResult.ResultConnectionEnum.Correct)
			{
				RegistrationResultDataJson jsonDataError = new RegistrationResultDataJson(
					registrationResult.LoginIsCorrect, registrationResult.LoginErrorText, 
					registrationResult.PasswordIsCorrect, registrationResult.PasswordErrorText,
					registrationResult.FirstNameIsCorrect, registrationResult.FirstNameErrorText, 
					registrationResult.LastNameIsCorrect, registrationResult.LastNameErrorText, 
					registrationResult.EmailIsCorrect, registrationResult.EmailErrorText, 
					registrationResult.PhoneIsCorrect, registrationResult.PhoneErrorText);

				string serializedDataError = JsonConvert.SerializeObject(jsonDataError);

				BaseJsonResult jsonResultError = new BaseJsonResult(true, false, registrationResult.Message, serializedDataError);

				string serializeError = JsonConvert.SerializeObject(jsonResultError);
				return serializeError;
			}

			
			BaseActionResult saveResult = _registrationBlModel.SaveNewUser(registrationResult.User);

			if (saveResult.ResultConnection != BaseActionResult.ResultConnectionEnum.Correct)
			{
				RegistrationResultDataJson jsonDataError = new RegistrationResultDataJson(
					registrationResult.LoginIsCorrect, registrationResult.LoginErrorText, 
					registrationResult.PasswordIsCorrect, registrationResult.PasswordErrorText,
					registrationResult.FirstNameIsCorrect, registrationResult.FirstNameErrorText, 
					registrationResult.LastNameIsCorrect, registrationResult.LastNameErrorText,
					registrationResult.EmailIsCorrect, registrationResult.EmailErrorText, 
					registrationResult.PhoneIsCorrect, registrationResult.PhoneErrorText);

				string serializedDataError = JsonConvert.SerializeObject(jsonDataError);

				BaseJsonResult jsonResultError = new BaseJsonResult(true, false, registrationResult.Message, serializedDataError);

				string serializeError = JsonConvert.SerializeObject(jsonResultError);
				return serializeError;
			}

			RegistrationResultDataJson jsonDataCorrect = new RegistrationResultDataJson(
				registrationResult.LoginIsCorrect, registrationResult.LoginErrorText, 
				registrationResult.PasswordIsCorrect, registrationResult.PasswordErrorText,
				registrationResult.FirstNameIsCorrect, registrationResult.FirstNameErrorText, 
				registrationResult.LastNameIsCorrect, registrationResult.LastNameErrorText,
				registrationResult.EmailIsCorrect, registrationResult.EmailErrorText, 
				registrationResult.PhoneIsCorrect, registrationResult.PhoneErrorText);


			string serializedDataCorrect = JsonConvert.SerializeObject(jsonDataCorrect);

			BaseJsonResult jsonResult = new BaseJsonResult(true, true, "Correct registration", serializedDataCorrect);

			string serializedResult = JsonConvert.SerializeObject(jsonResult);
			return serializedResult;

		}
	}
}
