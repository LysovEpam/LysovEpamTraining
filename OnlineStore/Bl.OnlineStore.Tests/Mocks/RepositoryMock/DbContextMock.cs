using BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock
{
	public class DbContextMock : IDbContext
	{
		

		private UserAccessRepositoryMock _userAccessRepositoryMock;
		private UserRepositoryMock _userRepositoryMock;
		private UserAuthorizationRepositoryMock _userAuthorizationRepositoryMock;
		private ProductCategoryRepositoryMock _productCategoryRepositoryMock;
		private ProductListRepositoryMock _productListRepositoryMock;
		private ProductRepositoryMock _productRepositoryMock;
		private UserOrderRepositoryMock _userOrderRepositoryMock;


		
		public IRepositoryUserAccess UserAccesses
		{
			get
			{
				if (_userAccessRepositoryMock == null)
					_userAccessRepositoryMock = new UserAccessRepositoryMock();

				return _userAccessRepositoryMock;
			}
		}
		public IRepositoryUser Users
		{
			get
			{
				if (_userRepositoryMock == null)
					_userRepositoryMock = new UserRepositoryMock(UserAccesses);

				return _userRepositoryMock;
			}
		}
		public IRepositoryUserAuthorization UserAuthorizations
		{
			get
			{
				if (_userAuthorizationRepositoryMock == null)
					_userAuthorizationRepositoryMock = new UserAuthorizationRepositoryMock();

				return _userAuthorizationRepositoryMock;
			}
		}

		public IRepository<ProductCategory> ProductCategories
		{
			get
			{
				if (_productCategoryRepositoryMock == null)
					_productCategoryRepositoryMock = new ProductCategoryRepositoryMock();

				return _productCategoryRepositoryMock;
			}
		}
		public IRepository<ProductList> ProductLists
		{
			get
			{
				if (_productListRepositoryMock == null)
					_productListRepositoryMock = new ProductListRepositoryMock();

				return _productListRepositoryMock;
			}
		}
		public IRepository<Product> Products
		{
			get
			{
				if (_productRepositoryMock == null)
					_productRepositoryMock = new ProductRepositoryMock();

				return _productRepositoryMock;
			}
		}
		public IRepository<UserOrder> UserOrders
		{
			get
			{
				if (_userOrderRepositoryMock == null)
					_userOrderRepositoryMock = new UserOrderRepositoryMock();

				return _userOrderRepositoryMock;
			}
		}

		

		
	}
}
