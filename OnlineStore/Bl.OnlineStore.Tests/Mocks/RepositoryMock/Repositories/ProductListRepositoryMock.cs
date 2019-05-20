using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class ProductListRepositoryMock : IRepository<ProductList>
	{
		
		public int? Create(ProductList item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
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

		public bool Update(ProductList item)
		{
			throw new NotImplementedException();
		}
	}
}
