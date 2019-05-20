using System;
using System.Collections.Generic;

namespace DALContracts.Repositories
{
	public interface IRepositoryCache<T> where T : class
	{
		List<T> GetAll();
		T GetById(int id);
		List<T> Find(Func<T, bool> predicate);
	}
}
