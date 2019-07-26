using BL.OnlineStore.Services;
using Bl.OnlineStore.Tests.Mocks;
using BL.OnlineStore.Tests.Mocks;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities.Additional;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bl.OnlineStore.Tests.SystemBlModel
{
	[TestClass]
	public class AuthorizationTest
	{
		
		private static AuthorizationService _authorizationService;
		private static ISessionTokenGenerate _sessionTokenMock;
		[ClassInitialize]
		public static void Init(TestContext testContext)
		{

			var passwordHash = new PasswordHashMock();
			var dbContext = new DbContextMock();
			_sessionTokenMock = new SessionTokenGenerateMock();

			_authorizationService = new AuthorizationService(dbContext, passwordHash, _sessionTokenMock);
		}

		#region CheckAuthorization --- CorrectParameters

		[TestMethod]
		public void CheckAuthorization_CorrectParameters()
		{

			var request = new AuthorizationRequest()
			{
				Login = "admin",
				Password = "12345678",
			};

			var result = _authorizationService.CheckAuthorization(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.Correct);

		}

		#endregion
		#region CheckAuthorization --- User not found

		[TestMethod]
		public void CheckAuthorization_UserNotFound()
		{

			var request = new AuthorizationRequest()
			{
				Login = "testNotExistUser",
				Password = "notExistPassword",
			};

			var result = _authorizationService.CheckAuthorization(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.SystemError &&
			              result.Message == "User with this login and password not found");

		}

		#endregion
		#region CheckAuthorization --- User block

		[TestMethod]
		public void CheckAuthorization_UserBlock()
		{

			var request = new AuthorizationRequest()
			{
				Login = "userblock",
				Password = "12345678",
			};

			var result = _authorizationService.CheckAuthorization(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.SystemError &&
			              result.Message == "The user is blocked");

		}

		#endregion
		#region CheckAuthorization --- User delete

		[TestMethod]
		public void CheckAuthorization_UserDelete()
		{

			var request = new AuthorizationRequest()
			{
				Login = "userdelete",
				Password = "12345678",
			};

			var result = _authorizationService.CheckAuthorization(request);

			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.SystemError &&
			              result.Message == "The user deleted");

		}

		#endregion


		#region AuthorizationUser --- AuthorizationAdmin

		[TestMethod]
		public void Authorization_AuthorizationAdmin()
		{

			var request = new AuthorizationRequest()
			{
				Login = "admin",
				Password = "12345678",
			};

			var result = _authorizationService.AuthorizationUser(request);

			Assert.IsTrue(result.userRole.Role == UserRole.RoleEnum.Admin &&
			              result.sessionToken == _sessionTokenMock.GenerateSessionToken(request.Login));

		}

		#endregion
		#region AuthorizationUser --- AuthorizationUser

		[TestMethod]
		public void Authorization_AuthorizationUser()
		{

			var request = new AuthorizationRequest()
			{
				Login = "user",
				Password = "12345678",
			};

			var result = _authorizationService.AuthorizationUser(request);

			Assert.IsTrue(result.userRole.Role == UserRole.RoleEnum.User &&
			              result.sessionToken == _sessionTokenMock.GenerateSessionToken(request.Login));

		}

		#endregion
	}
}