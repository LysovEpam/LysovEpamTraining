using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductCategoryRepository : ExecuteCommandBase, IRepository<ProductCategory>
	{

		private readonly string _connectionString;

		public ProductCategoryRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}
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
