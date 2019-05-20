using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class ProductCategoryRepositoryMock : IRepository<ProductCategory>
	{

		
		public int? Create(ProductCategory item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
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

		public bool Update(ProductCategory item)
		{
			throw new NotImplementedException();
		}
	}
}
