using BLContracts.ActionResults;
using BLContracts.Models;

namespace BLContracts.Services
{
	public interface IRegistrationService
	{
		ServiceResult CreateNewUser(RegistrationRequest dataRequest);

	}
}
