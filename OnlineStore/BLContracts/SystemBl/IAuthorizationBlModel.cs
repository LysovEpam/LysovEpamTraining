using CommonEntities.Additional;

namespace BLContracts.SystemBl
{
	public interface IAuthorizationBlModel
	{
		UserStatus CheckUserStatus(string login, string password);
		(UserStatus,string) GetAuthorizationToken(string login, string password);
	}
}
