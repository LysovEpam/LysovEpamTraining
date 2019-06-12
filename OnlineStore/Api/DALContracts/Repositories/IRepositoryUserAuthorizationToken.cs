using System;
using CommonEntities;
using CommonEntities.Additional;

namespace DALContracts.Repositories
{
	public interface IRepositoryUserAuthorizationToken : IRepository<UserAuthorizationToken>
	{
		bool CancelSessionKeys(int idUser, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus);
		bool CancelSessionKeys(UserSystem userSystem, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus);

		UserAuthorizationToken GetByToken(string token);
	}
}
