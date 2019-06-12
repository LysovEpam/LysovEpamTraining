using System.Collections.Generic;
using BL.OnlineStore.Tests.Mocks.RepositoryMock.RepositoriesCache;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock
{
	public class DbCacheAdapterMock : IDbCacheAdapter
	{
		private readonly IDbContext _dbContext;

		private ProductCategoryRepositoryCacheMock _productCategoryRepository;
		private ProductListRepositoryCacheMock _productListRepository;
		private ProductRepositoryCacheMock _productRepository;


		public DbCacheAdapterMock(IDbContext dbContext)
		{
			_dbContext = dbContext;

			_productCategoryRepository = null;
			_productListRepository = null;
			_productRepository = null;
		}

		
		public IRepositoryUserAccess UserAccesses => _dbContext.UserAccesses;
		public IRepositoryUserSystem UsersSystem => _dbContext.UsersSystem;
		public IRepositoryUserAuthorizationToken UserAuthorizationsToken => _dbContext.UserAuthorizationsToken;
		public IRepository<ProductCategory> ProductCategories
		{
			get
			{
				_productCategoryRepository = null;
				_productListRepository = null;
				_productRepository = null;

				return _dbContext.ProductCategories;
			}
		}
		public IRepository<ProductInformation> ProductInformations
		{
			get
			{
				_productListRepository = null;
				_productRepository = null;

				return _dbContext.ProductInformations;
			}
		}
		public IRepository<Product> Products
		{
			get
			{
				_productRepository = null;

				return _dbContext.Products;
			}
		}
		public IRepository<UserOrder> UserOrders => _dbContext.UserOrders;


		public IRepositoryCache<ProductCategory> ProductCategoriesCache
		{
			get
			{
				if (_productCategoryRepository == null)
				{
					IEnumerable<ProductCategory> list = ProductCategories.SelectAll();
					_productCategoryRepository = new ProductCategoryRepositoryCacheMock(list);
				}

				return _productCategoryRepository; 

			}
		}
		public IRepositoryCache<ProductInformation> ProductListsCache
		{
			get
			{
				if (_productListRepository == null)
				{
					IEnumerable<ProductInformation> list = ProductInformations.SelectAll();
					_productListRepository = new ProductListRepositoryCacheMock(list);
				}

				return _productListRepository;

			}
		}
		public IRepositoryCache<Product> ProductsCache
		{
			get
			{
				if (_productRepository == null)
				{
					IEnumerable<Product> list = Products.SelectAll();
					_productRepository = new ProductRepositoryCacheMock(list);
				}

				return _productRepository;

			}
		}

		public event RepositoryEvent RepositoryEvent;
	}
}
