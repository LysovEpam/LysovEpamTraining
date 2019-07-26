using BLContracts;
using CommonEntities;

namespace BL.OnlineStore.Tests.Mocks
{
	public class PasswordHashMock : IPasswordHash
	{
		public string GeneratePasswordHash(string login, string password)
		{
			string result = $"hash:{login}-{password}";

			return result;
		}
	}
}
