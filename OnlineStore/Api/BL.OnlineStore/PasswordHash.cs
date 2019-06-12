using System;
using BLContracts;

namespace BL.OnlineStore
{
	public class PasswordHash : IPasswordHash

	{

		private static string CryptSeed =
			"Ku6d7Qt31hY8A0U0Ha1oUEJw22U5U02iMU7h2nvRrSnxgxzWB08sYEXgSPohc17N38rP4F9M4EO8Cbj2xiA2s5GlZ5wv9UY2OQya" +
			"4RgKcFsqbDQtJcaDl91OpIIPdgJWHTQYikZX4PtB2cL6UHRcO0xOIUw7d2wOvXtVVx1ZHpCu9jnOzG7UZNiiMEWcYUtVbiojO5YV" +
			"kDZqmaAooFlJRtvfkxl6JNpFRiZAC25DJtdN7sKL2LAdIkHaiMiqrWETSyzH6dS7d33n0eINMEOBxolrlrTkIDQRbK5YpsUgyT0r" +
			"gQSN6b7ogyIEz6IdMQM3aHfYvIs4TCRTwbKDyNbBGXH5Nb5zN0Vt3uOoK4PKnMSoKAuQfrtPV3zY2aVlp5ShctPcsxYRxPUWZdLz" +
			"YSMOpfCLzM6lH683ClT4eu1jLzzxO5XAlC7w0dnECV0IYmKDh94C75peRxhmy0qvM1nT1zjtdCyYzlrZMISwCqNDRFUowKvtMyIY";

		private readonly int _passwordHashLength;

		public PasswordHash(int passwordHashLength)
		{
			_passwordHashLength = passwordHashLength;
		}

		/// <inheritdoc />
		public string GeneratePasswordHash(string login, string password)
		{
			//ЗАМЕНИТЬ! 
			//Закончить хеш функцию ( Переделать или доделать)

			#region Проверка входных параметров

			if (string.IsNullOrEmpty(login))
				throw new ArgumentException($"parameter '{nameof(login)}' cannot be empty", nameof(login));
			if (string.IsNullOrEmpty(password))
				throw new ArgumentException($"parameter '{nameof(password)}' cannot be empty", nameof(password));

			#endregion

			string tempResult = "";

			int summLogin = GetSummChar(login, CryptSeed) % 997;
			int summPassword = GetSummChar(password, CryptSeed) % 997;


			for (int i = 0; i < _passwordHashLength; i++)
			{
				int tempIndex = 451 + summLogin * summPassword + CryptSeed.Length;

				tempIndex = (tempIndex + (i * 2 + 1) * summLogin + (i * 2 + 1) * summPassword) % CryptSeed.Length;

				//Добавить к результату символ из опорной строки
				tempResult += CryptSeed[tempIndex];
				//Добавить к результату символ из логина
				tempIndex = (tempIndex + (i * 2 + 1) * summLogin + (i * 2 + 1) * summPassword) % CryptSeed.Length;
				tempResult += login.Length > i ? login[i] : CryptSeed[tempIndex];
				//Добавить к результату символ из опорной строки
				tempIndex = (tempIndex + (i * 2 + 1) * summLogin + (i * 2 + 1) * summPassword) % CryptSeed.Length;
				tempResult += CryptSeed[tempIndex];
			}

			//return tempResult;

			string result = "";

			for (int i = 0; i < _passwordHashLength; i++)
			{
				int tempIndex = 451 + summLogin * summPassword + CryptSeed.Length;

				tempIndex = (tempIndex + (i * 2 + 1) * summLogin + (i * 2 + 1) * summPassword) % CryptSeed.Length;

				result += tempResult.Length > i ? tempResult[i] : CryptSeed[tempIndex];

				if (result.Length >= _passwordHashLength)
					break;

				tempIndex = (tempIndex + (i * 2 + 1) * summLogin + (i * 2 + 1) * summPassword) % CryptSeed.Length;

				result += CryptSeed[tempIndex];

				if (result.Length >= _passwordHashLength)
					break;

			}

			return result;
		}

		private static int GetSummChar(string stringValue, string cryptSeed)
		{
			int valueSumm = 0;


			string stringValueLow = stringValue.ToLower();
			string cryptSeedLow = cryptSeed.ToLower();

			for (int i = 0; i < stringValueLow.Length; i++)
			{

				for (int j = 0; j < cryptSeedLow.Length; j++)
				{
					if (stringValueLow[i] == cryptSeedLow[j])
						valueSumm += i + j;
				}

			}

			return valueSumm;
		}
	}
}

