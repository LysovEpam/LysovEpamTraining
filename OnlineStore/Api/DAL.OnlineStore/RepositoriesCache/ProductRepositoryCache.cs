using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class ProductRepositoryCache : IRepository<Product>
	{
		private readonly ProductRepository _productRepository;

		private static List<Product> _cacheList;

		public ProductRepositoryCache(ProductRepository productRepository)
		{
			_productRepository = productRepository;
			_cacheList = new List<Product>();
			_cacheList = _productRepository.SelectAll();
		}


		public Product SelectById(int id)
		{
			// ReSharper disable once PossibleInvalidOperationException
			var category = _cacheList.Where(c => c.IdEntity.Value == id);

			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			// ReSharper disable once PossibleMultipleEnumeration
			if (category != null && category.Count() == 1)
				// ReSharper disable once PossibleMultipleEnumeration
				return category.FirstOrDefault();

			var categoryDb = _productRepository.SelectById(id);

			_cacheList.Add(categoryDb);

			return categoryDb;
		}
		public List<Product> SelectAll()
		{
			return _cacheList;
		}
		public List<Product> Find(Func<Product, bool> predicate)
		{
			var categories = _cacheList.Where(predicate);

			return categories.ToList();
		}


		public int? Insert(Product item)
		{
			var insertResult = _productRepository.Insert(item);

			if (insertResult.HasValue)
			{
				var newCategory = SelectById(insertResult.Value);
				_cacheList.Add(newCategory);
			}

			return insertResult;
		}
		public bool Update(Product item)
		{
			var updateResult = _productRepository.Update(item);

			if (updateResult)
			{
				// ReSharper disable once PossibleInvalidOperationException
				_cacheList[_cacheList.FindIndex(c => c.IdEntity.Value == item.IdEntity.Value)] = item;
			}



			return updateResult;
		}
		public bool Delete(int id)
		{
			var deleteResult = _productRepository.Delete(id);

			if (deleteResult)
			{
				// ReSharper disable once PossibleInvalidOperationException
				var item = _cacheList.SingleOrDefault(c => c.IdEntity.Value == id);
				if (item != null)
					_cacheList.Remove(item);
			}


			return deleteResult;
		}


		

		public int GetCountDependencies(int id)
		{
			return _productRepository.GetCountDependencies(id);
		}
	}
}
