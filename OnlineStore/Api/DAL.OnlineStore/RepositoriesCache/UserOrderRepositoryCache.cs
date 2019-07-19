using System;
using System.Collections.Generic;
using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class UserOrderRepositoryCache : IRepository<UserOrder>
	{
		private readonly UserOrderRepository _userOrderRepository;


		public UserOrderRepositoryCache(UserOrderRepository userOrderRepository)
		{
			_userOrderRepository = userOrderRepository;
		}

		
		public int? Insert(UserOrder item)
		{
			return _userOrderRepository.Insert(item);

		}
		public bool Update(UserOrder item)
		{
			return _userOrderRepository.Update(item);
		}

		public bool Delete(int id)
		{
			return _userOrderRepository.Delete(id);
		}


		public UserOrder SelectById(int id)
		{
			return _userOrderRepository.SelectById(id);
		}
		public List<UserOrder> SelectAll()
		{
			return _userOrderRepository.SelectAll();
		}
		public List<UserOrder> Find(Func<UserOrder, bool> predicate)
		{
			return _userOrderRepository.Find(predicate);
		}




		

		public int GetCountDependencies(int id)
		{
			return _userOrderRepository.GetCountDependencies(id);
		}
	}
}
