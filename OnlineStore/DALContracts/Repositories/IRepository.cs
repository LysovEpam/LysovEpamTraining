using System;
using System.Collections.Generic;

namespace DALContracts.Repositories
{
	public interface IRepository<T> where T : class
	{
		List<T> GetAll();
		T GetById(int id);
		List<T> Find(Func<T, bool> predicate); 
		int? Create(T item);
		bool Update(T item);
		bool Delete(int id);
	}
}
