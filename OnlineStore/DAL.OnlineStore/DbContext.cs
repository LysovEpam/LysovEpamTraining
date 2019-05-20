using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore
{
	public class DbContext : IDbContext
	{
		private readonly string _stringConnection;

		private UserAccessRepository _userAccessRepository;
		private UserRepository _userRepository;
		private UserAuthorizationRepository _userAuthorizationRepository;
		private ProductCategoryRepository _productCategoryRepository;
		private ProductListRepository _productListRepository;
		private ProductRepository _productRepository;
		private UserOrderRepository _userOrderRepository;


		
		public IRepositoryUserAccess UserAccesses
		{
			get
			{
				if (_userAccessRepository == null)
					_userAccessRepository = new UserAccessRepository(_stringConnection);

				return _userAccessRepository;
			}
		}
		public IRepositoryUser Users
		{
			get
			{
				if (_userRepository == null)
					_userRepository = new UserRepository(_stringConnection);

				return _userRepository;
			}
		}
		public IRepositoryUserAuthorization UserAuthorizations
		{
			get
			{
				if (_userAuthorizationRepository == null)
					_userAuthorizationRepository = new UserAuthorizationRepository(_stringConnection);

				return _userAuthorizationRepository;
			}
		}

		public IRepository<ProductCategory> ProductCategories
		{
			get
			{
				if (_productCategoryRepository == null)
					_productCategoryRepository = new ProductCategoryRepository(_stringConnection);

				return _productCategoryRepository;
			}
		}
		public IRepository<ProductList> ProductLists
		{
			get
			{
				if (_productListRepository == null)
					_productListRepository = new ProductListRepository(_stringConnection);

				return _productListRepository;
			}
		}
		public IRepository<Product> Products
		{
			get
			{
				if (_productRepository == null)
					_productRepository = new ProductRepository(_stringConnection);

				return _productRepository;
			}
		}
		public IRepository<UserOrder> UserOrders
		{
			get
			{
				if (_userOrderRepository == null)
					_userOrderRepository = new UserOrderRepository(_stringConnection);

				return _userOrderRepository;
			}
		}

		

		public DbContext(string stringConnection)
		{
			_stringConnection = stringConnection;
		}
	}
}
