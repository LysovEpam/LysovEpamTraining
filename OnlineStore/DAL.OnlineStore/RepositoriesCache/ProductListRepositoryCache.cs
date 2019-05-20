using System;
using System.Collections.Generic;
using System.Text;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class ProductListRepositoryCache : IRepositoryCache<ProductList>
	{
		private IEnumerable<ProductList> _list;

		public ProductListRepositoryCache(IEnumerable<ProductList> list)
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
