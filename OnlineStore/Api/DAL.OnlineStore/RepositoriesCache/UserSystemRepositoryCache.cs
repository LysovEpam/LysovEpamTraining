using System;
using System.Collections.Generic;
using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class UserSystemRepositoryCache : IRepositoryUserSystem
	{
		
		private readonly UserSystemRepository _userSystemRepository;

		public UserSystemRepositoryCache(UserSystemRepository userSystemRepository)
		{
			_userSystemRepository = userSystemRepository;
		}

		public UserSystem GetUserByLoginPasswordhash(string login, string passwordHash)
		{
			return _userSystemRepository.GetUserByLoginPasswordhash(login, passwordHash);
		}


		public int? Insert(UserSystem item)
		{
			return _userSystemRepository.Insert(item);
		}
		public bool Update(UserSystem item)
		{
			return _userSystemRepository.Update(item);
		}
		public bool Delete(int id)
		{
			return _userSystemRepository.Delete(id);
		}


		public List<UserSystem> SelectAll()
		{
			return _userSystemRepository.SelectAll();
		}

		public UserSystem SelectById(int id)
		{
			return _userSystemRepository.SelectById(id);
		}
		public List<UserSystem> Find(Func<UserSystem, bool> predicate)
		{
			return _userSystemRepository.Find(predicate);
		}

		
		public int GetCountDependencies(int id)
		{
			return _userSystemRepository.GetCountDependencies(id);
		}

		public UserSystem GetUserByLogin(string login)
		{
			return _userSystemRepository.GetUserByLogin(login);
		}
	}
}
