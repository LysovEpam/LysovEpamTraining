using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.RepositoriesCache
{
	public class ProductListRepositoryCacheMock : IRepositoryCache<ProductInformation>
	{
		private IEnumerable<ProductInformation> _list;

		public ProductListRepositoryCacheMock(IEnumerable<ProductInformation> list)
		{
			_list = list;
		}

		public int? Insert(ProductInformation item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<ProductInformation> Find(Func<ProductInformation, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<ProductInformation> SelectAll()
		{
			throw new NotImplementedException();
		}

		public ProductInformation SelectById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(ProductInformation item)
		{
			throw new NotImplementedException();
		}
	}
}
