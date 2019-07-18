using BLContracts.ActionResults;
using BLContracts.Models;

namespace BLContracts.SystemService
{
	public interface IRegistrationService
	{
		ServiceResult CreateNewUser(RegistrationRequest dataRequest);
	}
}
