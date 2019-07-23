using BLContracts.ActionResults;
using BLContracts.Models;

namespace BLContracts.Services
{
	public interface IUserSystemService
	{
		(ServiceResult actionResult, SystemUserData systemUserData) GetUserInformation(string sessionToken,string login);
		ServiceResult UpdateUserByMyself(SystemUserData userData);
	}
}
