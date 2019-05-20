using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.RepositoriesCache
{
	public class ProductListRepositoryCacheMock : IRepositoryCache<ProductList>
	{
		private IEnumerable<ProductList> _list;

		public ProductListRepositoryCacheMock(IEnumerable<ProductList> list)
		{
			_list = list;
		}
		public List<ProductList> Find(Func<ProductList, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<ProductList> GetAll()
		{
			throw new NotImplementedException();
		}

		public ProductList GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
