using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IUserSystemBlModel
	{
		(BaseActionResult, UserSystem) GetById(int id);
		(BaseActionResult, List<UserSystem>) GetAll();
		(BaseActionResult, UserSystem) GetByLogin(string login);
		(BaseActionResult, UserSystem) GetBySelf(string sessionToken);
		BaseActionResult UpdateUser(string sessionToken, UserSystem user);
		BaseActionResult DeleteUser(string sessionToken, int id);

	}
}
