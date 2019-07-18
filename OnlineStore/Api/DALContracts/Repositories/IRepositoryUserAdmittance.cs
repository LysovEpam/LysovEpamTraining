using CommonEntities;

namespace DALContracts.Repositories
{
	public interface IRepositoryUserAdmittance : IRepository<UserAdmittance>
	{
		UserAdmittance GetUserAdmittance(string login, string passwordHash);
		bool LoginUserIsUnique(string login);
	}
}
