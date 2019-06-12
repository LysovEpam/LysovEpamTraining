using BLContracts.ActionResults;
using CommonEntities.Additional;

namespace BLContracts.SystemBl
{
	public interface IAuthorizationBlModel
	{
		BaseActionResult CheckAuthorization(string login, string password);
		UserRole GetUserRole(string login, string password);
		
	}
}
