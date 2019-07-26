using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;

namespace BLContracts.Services
{
	public interface IUserSystemService
	{

		(ServiceResult actionResult, IEnumerable<SystemUserData> userData) GetAll();
		(ServiceResult actionResult, SystemUserData userData) GetById(int id);
		(ServiceResult actionResult, SystemUserData userData) GetByToken(string sessionToken);
		ServiceResult Create(string sessionToken, SystemUserData userData);
		ServiceResult CreatByRegistration(SystemUserData userData);
		ServiceResult Update(string sessionToken, SystemUserData userData);
		ServiceResult Delete(string sessionToken, int id);

	}
}
