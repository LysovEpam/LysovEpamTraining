using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserSystemRepositoryMock : IRepositoryUserSystem
	{
		private readonly List<UserSystem> _list;
		public UserSystemRepositoryMock(IRepositoryUserAdmittance repositoryUserAdmittance)
		{
			_list = new List<UserSystem>();

			for (int i = 1; i < 10; i++)
			{
				UserSystem userSystem = new UserSystem(i, $"firstName{i}", $"lastName{i}", $"email{i}", $"phone{i}", repositoryUserAdmittance.SelectById(i))
				{
					UserAdmittanceId = i
				};


				_list.Add(userSystem);
			}

		}
		public UserSystem GetUserByLoginPasswordhash(string login, string passwordHash)
		{
			var tempList = _list.Where(c => c.UserAdmittance.Login == login && c.UserAdmittance.PasswordHash == passwordHash);

			var list = tempList.ToList();

			if (list.Count != 1)
				return null;
			
			return list[0];
		}

		public int? Insert(UserSystem item)
		{
			int idEntity = _list[_list.Count - 1].IdEntity.Value;

			UserSystem userSystem = new UserSystem(idEntity + 1, item.FirsName, item.LastName, item.Email, item.Phone, item.UserAdmittance);

			_list.Add(userSystem);


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

		public List<UserSystem> Find(Func<UserSystem, bool> predicate)
		{
			var temp = _list.Where(predicate);
			return temp.ToList();
		}

		public List<UserSystem> SelectAll()
		{
			return _list;
		}

		public UserSystem SelectById(int id)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].IdEntity.Value == id)
					return _list[i];
			}

			return null;
		}

		

		public bool Update(UserSystem item)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].IdEntity.Value == item.IdEntity.Value)
				{
					_list[i].FirsName = item.FirsName;
					_list[i].LastName = item.LastName;
					_list[i].Email = item.Email;
					_list[i].Phone = item.Phone;
					_list[i].UserAdmittance = item.UserAdmittance;
					_list[i].UserAdmittanceId = item.UserAdmittanceId;

					return true;
				}
			}

			return false;
		}

		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}
	}
}
