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
	public class SystemUSerTest
	{
		private static UserSystemService _userSystemService;
		private static ISessionTokenGenerate _sessionTokenMock;
		[ClassInitialize]
		public static void Init(TestContext testContext)
		{
			var passwordHash = new PasswordHashMock();
			var dbContext = new DbContextMock();
			_sessionTokenMock = new SessionTokenGenerateMock();

			_userSystemService = new UserSystemService(passwordHash, dbContext);
		}

		#region  GetByToken --- CorrectParameters

		[TestMethod]
		public void GetByToken_CorrectParameters()
		{
			string userLogin = "admin";
			string userToken = _sessionTokenMock.GenerateSessionToken(userLogin);

			var result = _userSystemService.GetByToken(userToken);
			Assert.IsTrue(result.actionResult.ResultConnection == ServiceResult.ResultConnectionEnum.Correct);
		}

		#endregion
		#region  Create --- CorrectParameters

		[TestMethod]
		public void Create_CorrectParameters()
		{
			string userLogin = "admin";
			string userToken = _sessionTokenMock.GenerateSessionToken(userLogin);

			SystemUserData userData = new SystemUserData
			{
				Login = "newlogintest",
				Email = "test",
				FirstName = "test",
				LastName = "test",
				IdUser = 0,
				OldPassword = "12345678",
				NewPassword = "12345678", 
				Phone = "test",
				Role = new UserRole(UserRole.RoleEnum.User).GetRoleName(),
				Status = new UserStatus(UserStatus.StatusEnum.Active).GetStatusName()
			};
			
			var result = _userSystemService.Create(userToken, userData);
			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.Correct);
		}

		#endregion

		#region  Update --- CorrectParameters Admin

		[TestMethod]
		public void Update_CorrectParametersAdmin()
		{
			string userLogin = "admin";
			string userToken = _sessionTokenMock.GenerateSessionToken(userLogin);

			SystemUserData userData = new SystemUserData
			{
				Login = "editor",
				Email = "test",
				FirstName = "test",
				LastName = "test",
				IdUser = 2,
				OldPassword = "12345678",
				NewPassword = "12345678",
				Phone = "test",
				Role = new UserRole(UserRole.RoleEnum.User).GetRoleName(),
				Status = new UserStatus(UserStatus.StatusEnum.Active).GetStatusName()
			};

			var result = _userSystemService.Update(userToken, userData);
			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.Correct);
		}

		#endregion
		#region  Update --- Test update by myself

		[TestMethod]
		public void Update_ByMyself()
		{
			string userLogin = "admin";
			string userToken = _sessionTokenMock.GenerateSessionToken(userLogin);

			SystemUserData userData = new SystemUserData
			{
				Login = "admin",
				Email = "test",
				FirstName = "test",
				LastName = "test",
				IdUser = 1,
				OldPassword = "12345678",
				NewPassword = "12345678",
				Phone = "test",
				Role = new UserRole(UserRole.RoleEnum.Admin).GetRoleName(),
				Status = new UserStatus(UserStatus.StatusEnum.Active).GetStatusName()
			};

			var result = _userSystemService.Update(userToken, userData);
			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.Correct);
		}

		#endregion
		#region  Update --- Admin is trying to change his role

		[TestMethod]
		public void Update_AdminChangeHiRole()
		{
			string userLogin = "admin";
			string userToken = _sessionTokenMock.GenerateSessionToken(userLogin);

			SystemUserData userData = new SystemUserData
			{
				Login = "admin",
				Email = "test",
				FirstName = "test",
				LastName = "test",
				IdUser = 1,
				OldPassword = "12345678",
				NewPassword = "12345678",
				Phone = "test",
				Role = new UserRole(UserRole.RoleEnum.Editor).GetRoleName(),
				Status = new UserStatus(UserStatus.StatusEnum.Active).GetStatusName()
			};

			var result = _userSystemService.Update(userToken, userData);
			Assert.IsTrue(result.ResultConnection == ServiceResult.ResultConnectionEnum.AccessDenied);
		}

		#endregion
	}
}
