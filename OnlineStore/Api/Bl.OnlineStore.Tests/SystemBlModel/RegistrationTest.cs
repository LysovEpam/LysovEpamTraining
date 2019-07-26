using System.Collections.Generic;
using BL.OnlineStore;
using BL.OnlineStore.Services;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities;
using CommonEntities.Additional;
using DAL.OnlineStore;
using DAL.OnlineStore.Repositories;
using DALContracts;
using DALContracts.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bl.OnlineStore.Tests.SystemBlModel
{
	[TestClass]
	public class RegistrationTest
	{
		private static IPasswordHash _passwordHash;
		private static IDbContext _dbContext;
		private static RegistrationService _registrationService;

		[ClassInitialize]
		public static void Init(TestContext testContext)
		{
			

			UserSystem userAdmin = new UserSystem(1, "name", "name", "email", "phone",
				new UserAdmittance(1, "admin", "hashadmin",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Admin)));

			var mockRepository = new MockRepository(MockBehavior.Default);
			var systemRepositoryMock = mockRepository.Create<UserSystemRepository>();
			systemRepositoryMock.Setup(lw => lw.GetUserByLogin("admin")).Returns(userAdmin);


			
			var dbContextMock = mockRepository.Create<DbContext>();
			dbContextMock.Setup(db => db.UsersSystem).Returns(systemRepositoryMock.Object);

			_passwordHash = new PasswordHash();

			_registrationService = new RegistrationService(new UserSystemService(_passwordHash, dbContextMock.Object));
		}

		#region CreateNewUser --- CorrectParameters

		[TestMethod]
		public void CreateNewUser_CorrectParameters()
		{
			
			var request = new RegistrationRequest
			{
				Login = "admin",
				Password = "passwordTest",
				Email = "emailTest",
				Phone = "phoneTest",
				FirstName = "firstNameTest",
				LastName = "lastNameTest"
			};

			var result = _registrationService.CreateNewUser(request);

			Assert.IsTrue(result.ResultConnection != ServiceResult.ResultConnectionEnum.Correct);    //must be returned true

		}

		#endregion

		#region CreateNewUser --- Test empty parameters

		//[TestMethod]
		//public void CreateNewUser_LoginEmpty()
		//{
		//	string login = "";
		//	string password = "passwordTest";
		//	string firstName = "firstNameTest";
		//	string lastName = "lastNameTest";
		//	string email = "emailTest";
		//	string phone = "phoneTest";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&&*/ !result.LoginIsCorrect     //must be returned false!!!
		//				  && result.PasswordIsCorrect   //must be returned true
		//				  && result.FirstNameIsCorrect  //must be returned true
		//				  && result.LastNameIsCorrect   //must be returned true
		//				  && result.EmailIsCorrect      //must be returned true
		//				  && result.PhoneIsCorrect);    //must be returned true

		//}

		//[TestMethod]
		//public void CreateNewUser_PasswordEmpty()
		//{
		//	string login = "login";
		//	string password = "";
		//	string firstName = "firstNameTest";
		//	string lastName = "lastNameTest";
		//	string email = "emailTest";
		//	string phone = "phoneTest";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&& */result.LoginIsCorrect      //must be returned true
		//				  && !result.PasswordIsCorrect  //must be returned false!!!
		//				  && result.FirstNameIsCorrect  //must be returned true
		//				  && result.LastNameIsCorrect   //must be returned true
		//				  && result.EmailIsCorrect      //must be returned true
		//				  && result.PhoneIsCorrect);    //must be returned true

		//}

		//[TestMethod]
		//public void CreateNewUser_FirstNameEmpty()
		//{
		//	string login = "login";
		//	string password = "passwordTest";
		//	string firstName = "";
		//	string lastName = "lastNameTest";
		//	string email = "emailTest";
		//	string phone = "phoneTest";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&& */result.LoginIsCorrect      //must be returned true
		//				  && result.PasswordIsCorrect   //must be returned true
		//				  && !result.FirstNameIsCorrect //must be returned false!!!
		//				  && result.LastNameIsCorrect   //must be returned true
		//				  && result.EmailIsCorrect      //must be returned true
		//				  && result.PhoneIsCorrect);    //must be returned true

		//}

		//[TestMethod]
		//public void CreateNewUser_LastNameEmpty()
		//{
		//	string login = "login";
		//	string password = "passwordTest";
		//	string firstName = "firstNameTest";
		//	string lastName = "";
		//	string email = "emailTest";
		//	string phone = "phoneTest";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&&*/ result.LoginIsCorrect      //must be returned true
		//				  && result.PasswordIsCorrect   //must be returned true
		//				  && result.FirstNameIsCorrect  //must be returned true
		//				  && !result.LastNameIsCorrect  //must be returned false!!!
		//				  && result.EmailIsCorrect      //must be returned true
		//				  && result.PhoneIsCorrect);    //must be returned true

		//}

		//[TestMethod]
		//public void CreateNewUser_EmailEmpty()
		//{
		//	string login = "login";
		//	string password = "passwordTest";
		//	string firstName = "firstNameTest";
		//	string lastName = "lastNameTest";
		//	string email = "";
		//	string phone = "phoneTest";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&&*/ result.LoginIsCorrect      //must be returned true
		//				  && result.PasswordIsCorrect   //must be returned true
		//				  && result.FirstNameIsCorrect  //must be returned true
		//				  && result.LastNameIsCorrect   //must be returned true
		//				  && !result.EmailIsCorrect     //must be returned false!!!
		//				  && result.PhoneIsCorrect);    //must be returned true

		//}

		//[TestMethod]
		//public void CreateNewUser_PhoneEmpty()
		//{
		//	string login = "login";
		//	string password = "passwordTest";
		//	string firstName = "firstNameTest";
		//	string lastName = "lastNameTest";
		//	string email = "emailTest";
		//	string phone = "";

		//	RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

		//	Assert.IsTrue(//result.User == null  //user should not be returned
		//				  /*&&*/ result.LoginIsCorrect      //must be returned true
		//				  && result.PasswordIsCorrect   //must be returned true
		//				  && result.FirstNameIsCorrect  //must be returned true
		//				  && result.LastNameIsCorrect   //must be returned true
		//				  && result.EmailIsCorrect      //must be returned true
		//				  && !result.PhoneIsCorrect);   //must be returned false!!!

		//}


		#endregion




	}
}
