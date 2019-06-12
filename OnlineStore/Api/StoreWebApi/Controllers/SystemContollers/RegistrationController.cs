using BLContracts.ActionResults.System;
using BLContracts.SystemBl;
using Microsoft.AspNetCore.Mvc;

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
		public RegistrationResult RegistrationNewUser(string firstName, string lastName, string email, string phone, string login, string password)
		{
			RegistrationResult registrationResult = _registrationBlModel.CreateNewUser(firstName, lastName, email, phone, login, password);

			return registrationResult;

		}
	}
}
