using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IUserSystemService
	{
		(ServiceResult, UserSystem) GetById(int id);
		(ServiceResult, List<UserSystem>) GetAll();
		(ServiceResult, UserSystem) GetByLogin(string login);
		(ServiceResult, UserSystem) GetBySelf(string sessionToken);
		ServiceResult UpdateUser(string sessionToken, UserSystem user);
		ServiceResult DeleteUser(string sessionToken, int id);

	}
}
