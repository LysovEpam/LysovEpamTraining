using System;
using System.Collections.Generic;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;
using DALContracts.Repositories;
using Moq;

namespace Bl.OnlineStore.Tests.Mocks
{
	public class DbContextMock : IDbContext
	{
		private readonly IRepositoryUserAdmittance _userAdmittanceRepository;
		private readonly IRepositoryUserSystem _userSystemRepository;
		private readonly IRepositoryUserAuthorizationToken _userAuthorizationTokenRepository;
		private readonly IRepository<ProductCategory> _productCategoryRepository;
		private readonly IRepository<ProductInformation> _productInformationRepository;
		private readonly IRepository<Product> _productRepository;
		private readonly IRepository<UserOrder> _userOrderRepository;

		public DbContextMock()
		{


			#region UserAdmittance

			var listUserAdmittance = new List<UserAdmittance>
			{
				new UserAdmittance(1, "admin", "hash:admin-12345678",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Admin)),

				new UserAdmittance(2, "editor", "hash:editor-12345678",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Editor)),

				new UserAdmittance(3, "user", "hash:user-12345678",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.User)),

				new UserAdmittance(4, "userblock", "hash:userblock-12345678",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.User)),

				new UserAdmittance(5, "userdelete", "hash:userdelete-12345678",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.User))
			};

			

			Mock<IRepositoryUserAdmittance> userAdmittancesMock = new Mock<IRepositoryUserAdmittance>();



			userAdmittancesMock.Setup(action => action.LoginUserIsUnique(It.IsAny<string>())).Returns((string login) =>
			{
				foreach (var userAdmittance in listUserAdmittance)
				{
					if (login.ToLower() == userAdmittance.Login.ToLower())
						return false;
				}
				return true;
				
			});
			userAdmittancesMock.Setup(action => action.GetUserAdmittance(It.IsAny<string>(), It.IsAny<string>())).Returns((string login, string passwordHash) =>
			{
				foreach (var userAdmittance in listUserAdmittance)
				{
					if (login == userAdmittance.Login && passwordHash == userAdmittance.PasswordHash)
						return userAdmittance;
				}

				return null;
			});

			userAdmittancesMock.Setup(action => action.Insert(It.IsAny<UserAdmittance>())).Returns(1);
			userAdmittancesMock.Setup(action => action.Update(It.IsAny<UserAdmittance>())).Returns(true);
			userAdmittancesMock.Setup(action => action.Delete(It.IsAny<int>())).Returns(true);


			_userAdmittanceRepository = userAdmittancesMock.Object;

			#endregion

			#region UserSystem

			var listUserSystem = new List<UserSystem>
			{
				new UserSystem(1, "name1", "name1", "email1", "email1", listUserAdmittance[0]),
				new UserSystem(2, "name2", "name2", "email2", "emai2l", listUserAdmittance[1]),
				new UserSystem(3, "name3", "name3", "email3", "email3", listUserAdmittance[2]),
				new UserSystem(4, "name4", "name4", "email4", "email4", listUserAdmittance[3]),
				new UserSystem(5, "name5", "name5", "email5", "email5", listUserAdmittance[4])
			};

			

			Mock<IRepositoryUserSystem> userSystemMock = new Mock<IRepositoryUserSystem>();



			userSystemMock.Setup(action => action.Insert(It.IsAny<UserSystem>())).Returns(1);
			userSystemMock.Setup(action => action.Update(It.IsAny<UserSystem>())).Returns(true);
			userSystemMock.Setup(action => action.Delete(It.IsAny<int>())).Returns(true);
			userSystemMock.Setup(action => action.GetUserByLoginPasswordhash(It.IsAny<string>(), It.IsAny<string>())).Returns((string login, string passwordHash) =>
			{
				foreach (var systemUser in listUserSystem)
				{
					if (login == systemUser.UserAdmittance.Login &&
					    passwordHash == systemUser.UserAdmittance.PasswordHash)
						return systemUser;

				}

				return null;
			});

			_userSystemRepository = userSystemMock.Object;

			#endregion

			#region UserAuthorizationToken

			Mock<IRepositoryUserAuthorizationToken> userAuthorizationTokenMock = new Mock<IRepositoryUserAuthorizationToken>();

			userAuthorizationTokenMock.Setup(action => action.CancelSessionKeys(It.IsAny<int>(),
				It.IsAny<AuthorizationStatus>(), It.IsAny<DateTime>(), It.IsAny<AuthorizationStatus>())).Returns(true);
			userAuthorizationTokenMock.Setup(action => action.CancelSessionKeys(It.IsAny<UserSystem>(),
				It.IsAny<AuthorizationStatus>(), It.IsAny<DateTime>(), It.IsAny<AuthorizationStatus>())).Returns(true);

			userAuthorizationTokenMock.Setup(action => action.Insert(It.IsAny<UserAuthorizationToken>())).Returns(1);
			userAuthorizationTokenMock.Setup(action => action.Update(It.IsAny<UserAuthorizationToken>())).Returns(true);
			userAuthorizationTokenMock.Setup(action => action.Delete(It.IsAny<int>())).Returns(true);

			_userAuthorizationTokenRepository = userAuthorizationTokenMock.Object;

			#endregion





		}



		public IRepositoryUserAdmittance UserAdmittances => _userAdmittanceRepository;

		public IRepositoryUserSystem UsersSystem => _userSystemRepository;

		public IRepositoryUserAuthorizationToken UserAuthorizationsToken => _userAuthorizationTokenRepository;

		public IRepository<ProductCategory> ProductCategories => _productCategoryRepository;

		public IRepository<ProductInformation> ProductInformations => _productInformationRepository;

		public IRepository<Product> Products => _productRepository;

		public IRepository<UserOrder> UserOrders => _userOrderRepository;
	}
}
