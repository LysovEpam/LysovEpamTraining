using BLContracts;
using CommonEntities;

namespace BL.OnlineStore.Tests.Mocks
{
	public class PasswordHashMock : IPasswordHash
	{
		public string GeneratePasswordHash(string login, string password)
		{
			string result = "11111111112222222222333333333344444444445555555555";



			//string hash = $"{login}{password}";

			//

			//for (int i = 0; i < UserAccess.PasswrodHashLength; i++)
			//{
			//	if (hash.Length < i)
			//		result += hash[i];
			//	else
			//		result += "-";

			//}


			return result;
		}
	}
}
