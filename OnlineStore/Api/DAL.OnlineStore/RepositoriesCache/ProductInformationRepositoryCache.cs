using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class ProductInformationRepositoryCache : IRepository<ProductInformation>
	{
		private readonly ProductInformationRepository _productInformationRepository;
		private static List<ProductInformation> _cacheList;

		public ProductInformationRepositoryCache(ProductInformationRepository productInformationRepository)
		{
			_productInformationRepository = productInformationRepository;
			_cacheList = new List<ProductInformation>();
			_cacheList = _productInformationRepository.SelectAll();
		}

		

		public ProductInformation SelectById(int id)
		{
			// ReSharper disable once PossibleInvalidOperationException
			var information = _cacheList.Where(c => c.IdEntity.Value == id);

			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			// ReSharper disable once PossibleMultipleEnumeration
			if (information != null && information.Count() == 1)
				// ReSharper disable once PossibleMultipleEnumeration
				return information.FirstOrDefault();

			var categoryDb = _productInformationRepository.SelectById(id);

			_cacheList.Add(categoryDb);

			return categoryDb;

		}

		public List<ProductInformation> SelectAll()
		{
			return _cacheList;
		}

		public List<ProductInformation> Find(Func<ProductInformation, bool> predicate)
		{
			var categories = _cacheList.Where(predicate);

			return categories.ToList();
		}


		public int? Insert(ProductInformation item)
		{
			var insertResult = _productInformationRepository.Insert(item);

			if (insertResult.HasValue)
			{
				var newCategory = SelectById(insertResult.Value);
				_cacheList.Add(newCategory);
			}

			return insertResult;
		}

		public bool Update(ProductInformation item)
		{
			var updateResult = _productInformationRepository.Update(item);

			if (updateResult)
			{
				// ReSharper disable once PossibleInvalidOperationException
				_cacheList[_cacheList.FindIndex(c => c.IdEntity.Value == item.IdEntity.Value)] = item;
			}

			return updateResult;
		}

		public bool Delete(int id)
		{
			var deleteResult = _productInformationRepository.Delete(id);

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
			return _productInformationRepository.GetCountDependencies(id);
		}
	}
}
