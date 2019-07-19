using System;
using System.Collections.Generic;
using CommonEntities;
using CommonEntities.Additional;
using DAL.OnlineStore.Repositories;
using DALContracts.Repositories;

namespace DAL.OnlineStore.RepositoriesCache
{
	public class UserAuthorizationTokenRepositoryCache :  IRepositoryUserAuthorizationToken
	{

		
		private readonly UserAuthorizationTokenRepository _userAuthorizationTokenRepositor;

		public UserAuthorizationTokenRepositoryCache(UserAuthorizationTokenRepository userAuthorizationTokenRepositor) 
		{
			_userAuthorizationTokenRepositor = userAuthorizationTokenRepositor;
		}


		public bool CancelSessionKeys(int idUser, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
		{
			return _userAuthorizationTokenRepositor.CancelSessionKeys(idUser, oldStatus, dateBlock, newStatus);

		}
		public bool CancelSessionKeys(UserSystem userSystem, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
		{
			return _userAuthorizationTokenRepositor.CancelSessionKeys(userSystem, oldStatus, dateBlock, newStatus);
		}

		public UserAuthorizationToken GetByToken(string token)
		{
			return _userAuthorizationTokenRepositor.GetByToken(token);

		}

		public int? Insert(UserAuthorizationToken item)
		{
			return _userAuthorizationTokenRepositor.Insert(item);
		}
		public bool Update(UserAuthorizationToken item)
		{
			return _userAuthorizationTokenRepositor.Update(item);
		}
		public bool Delete(int id)
		{
			return _userAuthorizationTokenRepositor.Delete(id);
		}

		public UserAuthorizationToken SelectById(int id)
		{
			return _userAuthorizationTokenRepositor.SelectById(id);
		}
		public List<UserAuthorizationToken> SelectAll()
		{
			return _userAuthorizationTokenRepositor.SelectAll();
		}
		public List<UserAuthorizationToken> Find(Func<UserAuthorizationToken, bool> predicate)
		{
			return _userAuthorizationTokenRepositor.Find(predicate);
		}
		
		 
		public int GetCountDependencies(int id)
		{
			return _userAuthorizationTokenRepositor.GetCountDependencies(id);
		}
	}
}
