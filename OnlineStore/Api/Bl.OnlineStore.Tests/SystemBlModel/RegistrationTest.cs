using BL.OnlineStore.Services;
using Bl.OnlineStore.Tests.Mocks;
using BL.OnlineStore.Tests.Mocks;
using BLContracts.ActionResults;
using BLContracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bl.OnlineStore.Tests.SystemBlModel
{
	[TestClass]
	public class RegistrationTest
	{
		
		private static RegistrationService _registrationService;

		[ClassInitialize]
		public static void Init(TestContext testContext)
		{

			var passwordHash = new PasswordHashMock();
			var dbContext = new DbContextMock();

			_registrationService = new RegistrationService(new UserSystemService(passwordHash, dbContext));
		}

		#region CreateNewUser --- CorrectParameters

		[TestMethod]
		public void CreateNewUser_CorrectParameters()
		{
		

			var request = new RegistrationRequest
			{
				Login = "newTest",
				Password = "passwordTest",
				Email = "emailTest",
				Phone = "phoneTest",
				FirstName = "firstNameTest",
				LastName = "lastNameTest",

			};
			
			var result = _registrationService.CreateNewUser(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.Correct); 

		}

		#endregion

		#region CreateNewUser --- Test empty parameters

		[TestMethod]
		public void CreateNewUser_NotUniqueLogin()
		{

			var request = new RegistrationRequest
			{
				Login = "admin",
				Password = "passwordTest",
				Email = "emailTest",
				Phone = "phoneTest",
				FirstName = "firstNameTest",
				LastName = "lastNameTest",

			};

			var result = _registrationService.CreateNewUser(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.InvalidRequestData &&
			              result.Message == "User with the same name already exists!");   

		}

		#endregion




	}
}
