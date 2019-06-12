using CommonEntities;

namespace DALContracts.Repositories
{
	public interface IRepositoryUserAccess : IRepository<UserAccess>
	{
		UserAccess GetUserAccess(string login, string passwordHash);
		bool LoginUserIsUnique(string login);
	}
}
