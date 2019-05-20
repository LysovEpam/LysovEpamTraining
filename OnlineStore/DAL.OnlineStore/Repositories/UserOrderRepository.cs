using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class UserOrderRepository : ExecuteCommandBase, IRepository<UserOrder>
	{

		private readonly string _connectionString;

		public UserOrderRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}
		public int? Create(UserOrder item)
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

		public List<UserOrder> GetAll()
		{
			throw new NotImplementedException();
		}

		public UserOrder GetById(int id)
		{
			throw new NotImplementedException();
		}

		public bool Update(UserOrder item)
		{
			throw new NotImplementedException();
		}
	}
}
