using System;
using BLContracts;

namespace BL.OnlineStore
{
	public class SessionTokenGenerate: ISessionTokenGenerate
	{
		private readonly IPasswordHash _passwordHash;
		private readonly int _userTokenLength;

		public SessionTokenGenerate(IPasswordHash passwordHash)
		{
			_passwordHash = passwordHash;
			_userTokenLength = 50;
		}
		public string GenerateSessionToken(string login)
		{
			int random = new Random().Next(10000000, 99999999);
			var ticks = DateTime.Now.Ticks;
			string numb = $"{ticks}{random}";

			string loginHash = _passwordHash.GeneratePasswordHash(login, random.ToString());


			string result = "";

			for (int i = 0; result.Length < _userTokenLength; i++)
			{
				result += loginHash[i];
				result += numb[i];
			}

			return result;
		}
	}
}
