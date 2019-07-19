using CommonEntities;
using DAL.OnlineStore.Repositories;
using DAL.OnlineStore.RepositoriesCache;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore
{
	public class DbContextCache : IDbContext
	{
		private readonly UserAdmittanceRepositoryCache _userAdmittanceRepository;
		private readonly UserSystemRepositoryCache _userSystemRepository;
		private readonly UserAuthorizationTokenRepositoryCache _userAuthorizationTokenRepository;
		private readonly ProductCategoryRepositoryCache _productCategoryRepository;
		private readonly ProductInformationRepositoryCache _productInformationRepository;
		private readonly ProductRepositoryCache _productRepository;
		private readonly UserOrderRepositoryCache _userOrderRepository;




		public IRepositoryUserAdmittance UserAdmittances => _userAdmittanceRepository;
		public IRepositoryUserSystem UsersSystem => _userSystemRepository;
		public IRepositoryUserAuthorizationToken UserAuthorizationsToken => _userAuthorizationTokenRepository;


		public IRepository<ProductCategory> ProductCategories => _productCategoryRepository;
		public IRepository<ProductInformation> ProductInformations => _productInformationRepository;
		public IRepository<Product> Products => _productRepository;

		public IRepository<UserOrder> UserOrders => _userOrderRepository;


		public DbContextCache(string stringConnection)
		{
			var userAdmittanceRepositoryDb = new UserAdmittanceRepository(stringConnection);
			var userSystemRepositoryDb = new UserSystemRepository(stringConnection, userAdmittanceRepositoryDb);
			var userAuthorizationTokenRepositoryDb = new UserAuthorizationTokenRepository(stringConnection, userSystemRepositoryDb);

			var productCategoryRepositoryDb = new ProductCategoryRepository(stringConnection);
			var productInformationRepositoryDb = new ProductInformationRepository(stringConnection, productCategoryRepositoryDb);
			var productRepositoryDb = new ProductRepository(stringConnection, productInformationRepositoryDb);
			var userOrderRepositoryDb = new UserOrderRepository(stringConnection, productRepositoryDb, userSystemRepositoryDb);



			_userAdmittanceRepository = new UserAdmittanceRepositoryCache(userAdmittanceRepositoryDb);
			_userSystemRepository = new UserSystemRepositoryCache(userSystemRepositoryDb);
			_userAuthorizationTokenRepository = new UserAuthorizationTokenRepositoryCache(userAuthorizationTokenRepositoryDb);

			_productCategoryRepository = new ProductCategoryRepositoryCache(productCategoryRepositoryDb);
			_productInformationRepository = new ProductInformationRepositoryCache(productInformationRepositoryDb);
			_productRepository = new ProductRepositoryCache(productRepositoryDb);
			_userOrderRepository = new UserOrderRepositoryCache(userOrderRepositoryDb);

		}




	}
}
