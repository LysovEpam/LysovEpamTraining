﻿using BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock
{
	public class DbContextMock : IDbContext
	{
		

		private UserAccessRepositoryMock _userAccessRepositoryMock;
		private UserSystemRepositoryMock _userSystemRepositoryMock;
		private UserAuthorizationTokenRepositoryMock _userAuthorizationTokenRepositoryMock;
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
		public IRepositoryUserSystem UsersSystem
		{
			get
			{
				if (_userSystemRepositoryMock == null)
					_userSystemRepositoryMock = new UserSystemRepositoryMock(UserAccesses);

				return _userSystemRepositoryMock;
			}
		}
		public IRepositoryUserAuthorizationToken UserAuthorizationsToken
		{
			get
			{
				if (_userAuthorizationTokenRepositoryMock == null)
					_userAuthorizationTokenRepositoryMock = new UserAuthorizationTokenRepositoryMock();

				return _userAuthorizationTokenRepositoryMock;
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
		public IRepository<ProductInformation> ProductInformations
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

		public event RepositoryEvent RepositoryEvent;
	}
}
