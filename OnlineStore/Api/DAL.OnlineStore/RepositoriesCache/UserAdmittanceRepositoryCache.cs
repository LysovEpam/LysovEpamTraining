using System;
using System.Collections.Generic;
using CommonEntities;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class UserAdmittanceRepositoryCache : IRepositoryUserAdmittance
	{
		
		private readonly UserAdmittanceRepository _userAdmittanceRepository;

		public UserAdmittanceRepositoryCache(UserAdmittanceRepository userAdmittanceRepository) 
		{
			_userAdmittanceRepository = userAdmittanceRepository;
		}

		public UserAdmittance GetUserAdmittance(string login, string passwordHash)
		{
			return _userAdmittanceRepository.GetUserAdmittance(login, passwordHash);
		}
		public bool LoginUserIsUnique(string login)
		{
			return _userAdmittanceRepository.LoginUserIsUnique(login);
		}


		public int? Insert(UserAdmittance item)
		{
			return _userAdmittanceRepository.Insert(item);

		}
		public bool Update(UserAdmittance item)
		{
			return _userAdmittanceRepository.Update(item);
		}
		public bool Delete(int id)
		{
			return _userAdmittanceRepository.Delete(id);
		}


		public UserAdmittance SelectById(int id)
		{
			return _userAdmittanceRepository.SelectById(id);
		}
		public List<UserAdmittance> SelectAll()
		{
			return _userAdmittanceRepository.SelectAll();
		}
		public List<UserAdmittance> Find(Func<UserAdmittance, bool> predicate)
		{
			return _userAdmittanceRepository.Find(predicate);
		}



		public int GetCountDependencies(int id)
		{
			return _userAdmittanceRepository.GetCountDependencies(id);
		}
	}
}
