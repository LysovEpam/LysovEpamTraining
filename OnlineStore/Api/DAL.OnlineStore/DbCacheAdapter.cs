using System.Collections.Generic;
using CommonEntities;
using DAL.OnlineStore.RepositoriesCache;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore
{
	public class DbCacheAdapter: IDbCacheAdapter
	{
		private readonly IDbContext _dbContext;

		private ProductCategoryRepositoryCache _productCategoryRepository;
		private ProductListRepositoryCache _productListRepository;
		private ProductRepositoryCache _productRepository;


		public DbCacheAdapter(IDbContext dbContext)
		{
			_dbContext = dbContext;

			_productCategoryRepository = null;
			_productListRepository = null;
			_productRepository = null;
		}

		
		public IRepositoryUserAdmittance UserAdmittances => _dbContext.UserAdmittances;
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
					_productCategoryRepository = new ProductCategoryRepositoryCache(list);
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
					_productListRepository = new ProductListRepositoryCache(list);
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
					_productRepository = new ProductRepositoryCache(list);
				}

				return _productRepository;

			}
		}



		
	}
}
