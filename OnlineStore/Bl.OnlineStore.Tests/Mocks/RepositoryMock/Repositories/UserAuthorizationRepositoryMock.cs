using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserAuthorizationRepositoryMock : IRepositoryUserAuthorization
	{
		public bool CancelSessionKeys(int idUser)
		{
			return true;
		}

		public bool CancelSessionKeys(User user)
		{
			return true;
		}

		public int? Create(UserAuthorization item)
		{
			return null;
		}

		public bool Delete(int id)
		{
			return true;
		}

		public List<UserAuthorization> Find(Func<UserAuthorization, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<UserAuthorization> GetAll()
		{
			throw new NotImplementedException();
		}

		public UserAuthorization GetById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(UserAuthorization item)
		{
			throw new NotImplementedException();
		}
	}
}
