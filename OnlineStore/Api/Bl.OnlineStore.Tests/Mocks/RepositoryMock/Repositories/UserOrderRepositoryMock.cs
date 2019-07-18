using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserOrderRepositoryMock : IRepository<UserOrder>
	{

		
		public int? Insert(UserOrder item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<UserOrder> Find(Func<UserOrder, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public List<UserOrder> SelectAll()
		{
			throw new NotImplementedException();
		}

		public UserOrder SelectById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(UserOrder item)
		{
			throw new NotImplementedException();
		}

		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}
	}
}
