using BLContracts.ActionResults.System;

namespace BLContracts.SystemBl
{
	public interface IRegistrationBlModel
	{
		RegistrationResult CreateNewUser(string firstName, string lastName, string email, string phone, string login, string passwrod);
	}
}
