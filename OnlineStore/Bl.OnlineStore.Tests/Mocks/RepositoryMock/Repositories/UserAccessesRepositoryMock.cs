using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserAccessRepositoryMock : IRepositoryUserAccess
	{
		private readonly List<UserAccess> _list;

		public UserAccessRepositoryMock()
		{

			_list = new List<UserAccess>
			{
				new UserAccess(1, "login1", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Admin)),

				new UserAccess(2,"login2", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.Admin)),

				new UserAccess(3,"login3", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.Admin)),

				
				new UserAccess(4,"login4", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Editor)),

				new UserAccess(5,"login5", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.Editor)),

				new UserAccess(6, "login6", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.Editor)),


				new UserAccess(7,"login7", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.User)),

				new UserAccess(8,"login8", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.User)),

				new UserAccess(9,"login9", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.User)),

			};


		}

		public UserAccess GetUserAccess(string login, string passwordHash)
		{
			foreach (UserAccess userAccess in _list)
			{
				Console.WriteLine($"{userAccess.IdEntity}, {userAccess.Login}, {userAccess.PasswordHash}");

				
			}

			var tempList = _list.Where(c => c.Login == login && c.PasswordHash == passwordHash);

			var userAccesses = tempList.ToList();

			if (userAccesses.Count == 1)
				return userAccesses[0];

			return null;

		}
		public bool LoginUserIsUnique(string login)
		{
			var tempList = _list.Where(c => c.Login == login);

			if (tempList.Count() != 0)
				return false;

			return true;

		}

		public int? Create(UserAccess item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<UserAccess> Find(Func<UserAccess, bool> predicate)
		{
			var t = _list.Where(predicate);

			return t.ToList();
		}

		public List<UserAccess> GetAll()
		{
			return _list;
		}

		public UserAccess GetById(int id)
		{
			var t = _list.Where(c => c.IdEntity == id);

			var s = t.ToList();

			return s.Count == 1 ? s[0] : null;
		}



		public bool Update(UserAccess item)
		{
			throw new NotImplementedException();
		}


	}
}
