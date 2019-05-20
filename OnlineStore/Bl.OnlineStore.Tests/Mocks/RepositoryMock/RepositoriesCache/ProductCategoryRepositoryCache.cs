using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.RepositoriesCache
{
	class ProductCategoryRepositoryCacheMock : IRepositoryCache<ProductCategory>
	{
		private IEnumerable<ProductCategory> _list;
		public ProductCategoryRepositoryCacheMock(IEnumerable<ProductCategory> list)
		{
			_list = list;
		}
		public List<ProductCategory> Find(Func<ProductCategory, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<ProductCategory> GetAll()
		{
			throw new NotImplementedException();
		}

		public ProductCategory GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
