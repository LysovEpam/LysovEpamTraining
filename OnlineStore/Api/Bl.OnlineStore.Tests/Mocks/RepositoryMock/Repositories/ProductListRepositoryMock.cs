using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class ProductListRepositoryMock : IRepository<ProductInformation>
	{
		
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
