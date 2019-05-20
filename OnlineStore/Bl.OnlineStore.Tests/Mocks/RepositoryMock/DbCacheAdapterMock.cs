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
		public IRepositoryUser Users => _dbContext.Users;
		public IRepositoryUserAuthorization UserAuthorizations => _dbContext.UserAuthorizations;
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
		public IRepository<ProductList> ProductLists
		{
			get
			{
				_productListRepository = null;
				_productRepository = null;

				return _dbContext.ProductLists;
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
					IEnumerable<ProductCategory> list = ProductCategories.GetAll();
					_productCategoryRepository = new ProductCategoryRepositoryCacheMock(list);
				}

				return _productCategoryRepository; 

			}
		}
		public IRepositoryCache<ProductList> ProductListsCache
		{
			get
			{
				if (_productListRepository == null)
				{
					IEnumerable<ProductList> list = ProductLists.GetAll();
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
					IEnumerable<Product> list = Products.GetAll();
					_productRepository = new ProductRepositoryCacheMock(list);
				}

				return _productRepository;

			}
		}
	}
}
