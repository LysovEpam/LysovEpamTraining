using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductRepository : ExecuteCommandBase, IRepository<Product>
	{

		private readonly string _connectionString;

		public ProductRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}
		public int? Create(Product item)
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

		public List<Product> GetAll()
		{
			throw new NotImplementedException();
		}

		public Product GetById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(Product item)
		{
			throw new NotImplementedException();
		}
	}
}
