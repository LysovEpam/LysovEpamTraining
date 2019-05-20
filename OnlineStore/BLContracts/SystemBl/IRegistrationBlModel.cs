using BLContracts.ActionResults;
using BLContracts.ActionResults.System;
using CommonEntities;

namespace BLContracts.SystemBl
{
	public interface IRegistrationBlModel
	{
		RegistrationResult CreateNewUser(string firstName, string lastName, string email, string phone, string login, string passwrod);
		BaseActionResult SaveNewUser(User user);
	}
}
