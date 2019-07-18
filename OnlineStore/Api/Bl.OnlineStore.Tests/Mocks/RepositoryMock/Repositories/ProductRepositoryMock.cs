using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class ProductRepositoryMock : IRepository<Product>
	{

		
		public int? Insert(Product item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Product> Find(Func<Product, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<Product> SelectAll()
		{
			throw new NotImplementedException();
		}

		public Product SelectById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Product item)
		{
			throw new NotImplementedException();
		}

		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}
	}
}
