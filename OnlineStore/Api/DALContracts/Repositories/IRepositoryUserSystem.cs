using CommonEntities;

namespace DALContracts.Repositories
{
	public interface IRepositoryUserSystem : IRepository<UserSystem>
	{
		UserSystem GetUserByLoginPasswordhash(string login, string passwordHash);
	}
}
