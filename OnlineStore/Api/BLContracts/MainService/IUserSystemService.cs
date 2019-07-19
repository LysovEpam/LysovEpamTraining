using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IUserSystemService
	{
		(ServiceResult actionResult, SystemUserData systemUserData) GetUserInformation(string sessionToken,string login);
		ServiceResult UpdateUserByMyself(string sessionToken, SystemUserData userData);
	}
}
