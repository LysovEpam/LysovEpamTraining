using CommonEntities;

namespace DALContracts.Repositories
{
	public interface IRepositoryUser : IRepository<User>
	{
		User GetUserByLoginPasswordhash(string login, string passwordHash);
	}
}
