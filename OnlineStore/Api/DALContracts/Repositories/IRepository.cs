using System;
using System.Collections.Generic;

namespace DALContracts.Repositories
{
	public interface IRepository<T> where T : class
	{
		List<T> SelectAll();
		T SelectById(int id);
		List<T> Find(Func<T, bool> predicate); 
		int? Insert(T item);
		bool Update(T item);
		bool Delete(int id);
		int GetCountDependencies(int id);
	}
}
