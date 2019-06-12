using System;
using System.Collections.Generic;
using System.Text;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class ProductRepositoryCache : IRepositoryCache<Product>
	{
		private IEnumerable<Product> _list;

		public ProductRepositoryCache(IEnumerable<Product> list)
		{
			_list = list;
		}

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
	}
}
