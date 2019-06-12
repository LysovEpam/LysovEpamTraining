using BL.OnlineStore.BlModels.SystemBlModels;
using BL.OnlineStore.Tests.Mocks;
using BL.OnlineStore.Tests.Mocks.RepositoryMock;
using BLContracts;
using CommonEntities.Additional;
using DALContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bl.OnlineStore.Tests.SystemBlModel
{
	[TestClass]
	public class AuthorizationTest
	{
		private static IDbContext _dbContext;
		private static IPasswordHash _passwordHash;

		private static AuthorizationBlModel _authorizationBlModel;

		[ClassInitialize]
		public static void Init(TestContext testContext)
		{
			//_dbContext = new DbContextMock();
			//_passwordHash = new PasswordHashMock();

			//_authorizationBlModel = new AuthorizationBlModel(_dbContext, _passwordHash);
		}

		#region Check correct status

		//[TestMethod]
		//public void CheckUserStatus_CheckCorrectActiveStatus()
		//{
		//	string login = "login1";
		//	string password = "password1";

		//	UserStatus status = _authorizationBlModel.GetUserStatus(login, password);

		//	Assert.IsTrue(status.Status == UserStatus.StatusEnum.Active);
		//}

		//[TestMethod]
		//public void CheckUserStatus_CheckCorrectBlockStatus()
		//{
		//	string login = "login2";
		//	string password = "password1";

		//	UserStatus status = _authorizationBlModel.GetUserStatus(login, password);

		//	Assert.IsTrue(status.Status == UserStatus.StatusEnum.Block);
		//}

		//[TestMethod]
		//public void CheckUserStatus_CheckCorrectDeleteStatus()
		//{
		//	string login = "login3";
		//	string password = "password1";

		//	UserStatus status = _authorizationBlModel.GetUserStatus(login, password);

		//	Assert.IsTrue(status.Status == UserStatus.StatusEnum.Delete);
		//}

		#endregion
		#region Check uncorrect status

		//[TestMethod]
		//public void CheckUserStatus_CheckUncorrectLogin()
		//{
		//	string login = "login";
		//	string password = "password1";

		//	UserStatus status = _authorizationBlModel.GetUserStatus(login, password);

		//	Assert.IsTrue(status == null);
		//}

		//[TestMethod]
		//public void CheckUserStatus_CheckEmptyLogin()
		//{
		//	string login = "";
		//	string password = "password1";

		//	UserStatus status = _authorizationBlModel.GetUserStatus(login, password);

		//	Assert.IsTrue(status == null);
		//}

		#endregion
		#region Check generate token

		//[TestMethod]
		//public void GetAuthorizationToken_CheckActiveUser()
		//{
		//	string login = "login1";
		//	string password = "password1";

		//	(UserStatus, string) sessionKey = _authorizationBlModel.GetAuthorizationToken(login, password);

		//	Assert.IsTrue(sessionKey.Item1.Status == UserStatus.StatusEnum.Active &&
		//				  sessionKey.Item2 != null);
		//}

		//[TestMethod]
		//public void GetAuthorizationToken_CheckBlockUser()
		//{
		//	string login = "login2";
		//	string password = "password1";

		//	(UserStatus, string) sessionKey = _authorizationBlModel.GetAuthorizationToken(login, password);

		//	Assert.IsTrue(sessionKey.Item1.Status == UserStatus.StatusEnum.Block &&
		//				  sessionKey.Item2 == null);
		//}

		//[TestMethod]
		//public void GetAuthorizationToken_CheckDeleteUser()
		//{
		//	string login = "login3";
		//	string password = "password1";

		//	(UserStatus, string) sessionKey = _authorizationBlModel.GetAuthorizationToken(login, password);

		//	Assert.IsTrue(sessionKey.Item1.Status == UserStatus.StatusEnum.Delete &&
		//				  sessionKey.Item2 == null);
		//}

		//[TestMethod]
		//public void GetAuthorizationToken_CheckUncorrectUser()
		//{
		//	string login = "login";
		//	string password = "password";

		//	(UserStatus, string) sessionKey = _authorizationBlModel.GetAuthorizationToken(login, password);

		//	Assert.IsTrue(sessionKey.Item1 == null &&
		//				  sessionKey.Item2 == null);
		//}

		#endregion


	}
}
