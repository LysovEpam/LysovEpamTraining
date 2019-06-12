using System;
using System.Collections.Generic;
using System.Text;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	class ProductCategoryRepositoryCache : IRepositoryCache<ProductCategory>
	{
		private IEnumerable<ProductCategory> _list;
		public ProductCategoryRepositoryCache(IEnumerable<ProductCategory> list)
		{
			_list = list;
		}

		public int? Insert(ProductCategory item)
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

		public List<ProductCategory> SelectAll()
		{
			throw new NotImplementedException();
		}

		public ProductCategory SelectById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(ProductCategory item)
		{
			throw new NotImplementedException();
		}
	}
}
