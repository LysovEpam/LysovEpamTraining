using BLContracts;

namespace Bl.OnlineStore.Tests.Mocks
{
	public class SessionTokenGenerateMock : ISessionTokenGenerate
	{
		public string GenerateSessionToken(string login)
		{
			return $"{login}-token-test";
		}
	}
}
