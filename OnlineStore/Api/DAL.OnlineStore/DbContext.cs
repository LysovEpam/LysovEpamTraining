using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore
{
	public class DbContext : IDbContext
	{
		private readonly UserAdmittanceRepository _userAdmittanceRepository;
		private readonly UserSystemRepository _userSystemRepository;
		private readonly UserAuthorizationTokenRepository _userAuthorizationTokenRepository;
		private readonly ProductCategoryRepository _productCategoryRepository;
		private readonly ProductInformationRepository _productInformationRepository;
		private readonly ProductRepository _productRepository;
		private readonly UserOrderRepository _userOrderRepository;


		

		public IRepositoryUserAdmittance UserAdmittances => _userAdmittanceRepository;
		public IRepositoryUserSystem UsersSystem => _userSystemRepository;
		public IRepositoryUserAuthorizationToken UserAuthorizationsToken => _userAuthorizationTokenRepository;


		public IRepository<ProductCategory> ProductCategories => _productCategoryRepository;
		public IRepository<ProductInformation> ProductInformations => _productInformationRepository;
		public IRepository<Product> Products => _productRepository;

		public IRepository<UserOrder> UserOrders => _userOrderRepository;


		public DbContext(string stringConnection)
		{
			_userAdmittanceRepository = new UserAdmittanceRepository(stringConnection);
			_userSystemRepository = new UserSystemRepository(stringConnection, _userAdmittanceRepository);
			_userAuthorizationTokenRepository = new UserAuthorizationTokenRepository(stringConnection, _userSystemRepository);

			_productCategoryRepository = new ProductCategoryRepository(stringConnection);
			_productInformationRepository = new ProductInformationRepository(stringConnection, _productCategoryRepository);
			_productRepository = new ProductRepository(stringConnection, _productInformationRepository);
			_userOrderRepository = new UserOrderRepository(stringConnection, _productRepository, _userSystemRepository);

			

		}

		

		
	}
}
