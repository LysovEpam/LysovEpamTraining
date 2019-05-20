using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		// GET api/Users
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "User1", "User2" };
		}

		//Регистрация нового пользователя
		// POST api/values
		[HttpPost]
		//[Route("api/UsersController/{firstName}/{lastName}")]
		public string RegistrationNewUser(string firstName, string lastName)
		{
			return $"Test Registration {firstName}, {lastName}";
		}

		[HttpPost]
		public string TestUser(string firstName)
		{
			return $"Test user {firstName}";
		}


	}
}