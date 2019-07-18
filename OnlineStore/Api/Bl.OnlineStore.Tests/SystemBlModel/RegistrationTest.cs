﻿//using BL.OnlineStore;
//using BL.OnlineStore.BlModels.SystemBlModels;
//using BL.OnlineStore.Services.SystemServices;
//using BL.OnlineStore.Tests.Mocks.RepositoryMock;
//using BLContracts;
//using BLContracts.ActionResults.System;
//using CommonEntities;
//using DALContracts;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Bl.OnlineStore.Tests.SystemBlModel
//{
//	[TestClass]
//	public class RegistrationTest
//	{
//		private static IPasswordHash _passwordHash;
//		private static IDbContext _dbContext;
//		private static RegistrationService _registrationService;

//		[ClassInitialize]
//		public static void Init(TestContext testContext)
//		{
//			//_passwordHash = new PasswordHash(UserAccess.PasswordHashLength);
//			//_dbContext = new DbContextMock();

//			//_registrationBlModel = new RegistrationBlModel(_passwordHash, _dbContext);
//		}

//		#region CreateNewUser --- CorrectParameters

//		[TestMethod]
//		public void CreateNewUser_CorrectParameters()
//		{
//			string login = "login";
//			string password = "passwordTest";
//			string firstName = "firstNameTest";
//			string lastName = "lastNameTest";
//			string email = "emailTest";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User != null   //user must be returned
//						  /*&&*/ result.LoginIsCorrect      //must be returned true
//						  && result.PasswordIsCorrect   //must be returned true
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && result.LastNameIsCorrect   //must be returned true
//						  && result.EmailIsCorrect      //must be returned true
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		#endregion

//		#region CreateNewUser --- Test empty parameters

//		[TestMethod]
//		public void CreateNewUser_LoginEmpty()
//		{
//			string login = "";
//			string password = "passwordTest";
//			string firstName = "firstNameTest";
//			string lastName = "lastNameTest";
//			string email = "emailTest";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&&*/ !result.LoginIsCorrect     //must be returned false!!!
//						  && result.PasswordIsCorrect   //must be returned true
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && result.LastNameIsCorrect   //must be returned true
//						  && result.EmailIsCorrect      //must be returned true
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		[TestMethod]
//		public void CreateNewUser_PasswordEmpty()
//		{
//			string login = "login";
//			string password = "";
//			string firstName = "firstNameTest";
//			string lastName = "lastNameTest";
//			string email = "emailTest";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&& */result.LoginIsCorrect      //must be returned true
//						  && !result.PasswordIsCorrect  //must be returned false!!!
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && result.LastNameIsCorrect   //must be returned true
//						  && result.EmailIsCorrect      //must be returned true
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		[TestMethod]
//		public void CreateNewUser_FirstNameEmpty()
//		{
//			string login = "login";
//			string password = "passwordTest";
//			string firstName = "";
//			string lastName = "lastNameTest";
//			string email = "emailTest";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&& */result.LoginIsCorrect      //must be returned true
//						  && result.PasswordIsCorrect   //must be returned true
//						  && !result.FirstNameIsCorrect //must be returned false!!!
//						  && result.LastNameIsCorrect   //must be returned true
//						  && result.EmailIsCorrect      //must be returned true
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		[TestMethod]
//		public void CreateNewUser_LastNameEmpty()
//		{
//			string login = "login";
//			string password = "passwordTest";
//			string firstName = "firstNameTest";
//			string lastName = "";
//			string email = "emailTest";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&&*/ result.LoginIsCorrect      //must be returned true
//						  && result.PasswordIsCorrect   //must be returned true
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && !result.LastNameIsCorrect  //must be returned false!!!
//						  && result.EmailIsCorrect      //must be returned true
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		[TestMethod]
//		public void CreateNewUser_EmailEmpty()
//		{
//			string login = "login";
//			string password = "passwordTest";
//			string firstName = "firstNameTest";
//			string lastName = "lastNameTest";
//			string email = "";
//			string phone = "phoneTest";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&&*/ result.LoginIsCorrect      //must be returned true
//						  && result.PasswordIsCorrect   //must be returned true
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && result.LastNameIsCorrect   //must be returned true
//						  && !result.EmailIsCorrect     //must be returned false!!!
//						  && result.PhoneIsCorrect);    //must be returned true

//		}

//		[TestMethod]
//		public void CreateNewUser_PhoneEmpty()
//		{
//			string login = "login";
//			string password = "passwordTest";
//			string firstName = "firstNameTest";
//			string lastName = "lastNameTest";
//			string email = "emailTest";
//			string phone = "";

//			RegistrationResult result = _registrationService.CreateNewUser(firstName, lastName, email, phone, login, password);

//			Assert.IsTrue(//result.User == null  //user should not be returned
//						  /*&&*/ result.LoginIsCorrect      //must be returned true
//						  && result.PasswordIsCorrect   //must be returned true
//						  && result.FirstNameIsCorrect  //must be returned true
//						  && result.LastNameIsCorrect   //must be returned true
//						  && result.EmailIsCorrect      //must be returned true
//						  && !result.PhoneIsCorrect);   //must be returned false!!!

//		}


//		#endregion

		


//	}
//}
