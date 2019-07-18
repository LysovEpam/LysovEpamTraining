using System;
using System.Collections.Generic;
using System.Linq;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts.Repositories;

namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
{
	public class UserAdmittanceRepositoryMock : IRepositoryUserAdmittance
	{
		private readonly List<UserAdmittance> _list;

		public UserAdmittanceRepositoryMock()
		{

			_list = new List<UserAdmittance>
			{
				new UserAdmittance(1, "login1", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Admin)),

				new UserAdmittance(2,"login2", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.Admin)),

				new UserAdmittance(3,"login3", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.Admin)),

				
				new UserAdmittance(4,"login4", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.Editor)),

				new UserAdmittance(5,"login5", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.Editor)),

				new UserAdmittance(6, "login6", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.Editor)),


				new UserAdmittance(7,"login7", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Active), new UserRole(UserRole.RoleEnum.User)),

				new UserAdmittance(8,"login8", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Block), new UserRole(UserRole.RoleEnum.User)),

				new UserAdmittance(9,"login9", "11111111112222222222333333333344444444445555555555",
					new UserStatus(UserStatus.StatusEnum.Delete), new UserRole(UserRole.RoleEnum.User)),

			};


		}

		public UserAdmittance GetUserAdmittance(string login, string passwordHash)
		{
			foreach (UserAdmittance userAccess in _list)
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

		public int? Insert(UserAdmittance item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<UserAdmittance> Find(Func<UserAdmittance, bool> predicate)
		{
			var t = _list.Where(predicate);

			return t.ToList();
		}

		public List<UserAdmittance> SelectAll()
		{
			return _list;
		}

		public UserAdmittance SelectById(int id)
		{
			var t = _list.Where(c => c.IdEntity == id);

			var s = t.ToList();

			return s.Count == 1 ? s[0] : null;
		}



		public bool Update(UserAdmittance item)
		{
			throw new NotImplementedException();
		}

		public int GetCountDependencies(int id)
		{
			throw new NotImplementedException();
		}
	}
}
