using CommonEntities;

namespace DALContracts.Repositories
{
	public interface IRepositoryUserAuthorization : IRepository<UserAuthorization>
	{
		bool CancelSessionKeys(int idUser);
		bool CancelSessionKeys(User user);
	}
}
