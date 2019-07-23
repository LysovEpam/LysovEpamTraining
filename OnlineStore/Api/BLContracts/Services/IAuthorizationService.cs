using System;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities.Additional;

namespace BLContracts.Services
{
	public interface IAuthorizationService
	{
		ServiceResult CheckAuthorization(AuthorizationRequest authorizationData);
		(UserRole userRole, string sessionToken, DateTime authorizationFinish) AuthorizationUser(AuthorizationRequest authorizationData);

	}
}
