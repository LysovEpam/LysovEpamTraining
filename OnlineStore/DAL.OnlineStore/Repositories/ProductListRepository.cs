using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductListRepository : ExecuteCommandBase, IRepository<ProductList>
	{
		private readonly string _connectionString;

		public ProductListRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}
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
