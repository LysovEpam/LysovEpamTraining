using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserRepositoryMock : IRepositoryUser
	{
		private readonly List<User> _list;
		public UserRepositoryMock(IRepositoryUserAccess userAccessRepository)
		{
			_list = new List<User>();

			for (int i = 1; i < 10; i++)
			{
				User user = new User(i, $"firstName{i}", $"lastName{i}", $"email{i}", $"phone{i}",
					userAccessRepository.GetById(i));

				_list.Add(user);
			}

		}
		public User GetUserByLoginPasswordhash(string login, string passwordHash)
		{
			var tempList = _list.Where(c => c.UserAccess.Login == login && c.UserAccess.PasswordHash == passwordHash);

			var list = tempList.ToList();

			if (list.Count != 1)
				return null;
			
			return list[0];
		}

		public int? Create(User item)
		{
			int idEntity = _list[_list.Count - 1].IdEntity.Value;

			User user = new User(idEntity + 1, item.FirsName, item.LastName, item.Email, item.Phone, item.UserAccess);

			_list.Add(user);


			return idEntity + 1;
		}

		public bool Delete(int id)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].IdEntity.Value == id)
				{ 
					_list.RemoveAt(i);
					break;
				}
			}

			return true;
		}

		public List<User> Find(Func<User, bool> predicate)
		{
			var temp = _list.Where(predicate);
			return temp.ToList();
		}

		public List<User> GetAll()
		{
			return _list;
		}

		public User GetById(int id)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].IdEntity.Value == id)
					return _list[i];
			}

			return null;
		}

		

		public bool Update(User item)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].IdEntity.Value == item.IdEntity.Value)
				{
					_list[i].FirsName = item.FirsName;
					_list[i].LastName = item.LastName;
					_list[i].Email = item.Email;
					_list[i].Phone = item.Phone;
					_list[i].UserAccess = item.UserAccess;
					_list[i].UserAccessId = item.UserAccessId;

					return true;
				}
			}

			return false;
		}
	}
}
