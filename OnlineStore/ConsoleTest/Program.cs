using System;
using System.Collections.Generic;
using System.Text;
using BL.OnlineStore;
using BLContracts;
using CommonEntities;
using CommonEntities.Additional;
using DAL.OnlineStore;
using DALContracts;

namespace ConsoleTest
{
	internal class Program
	{
		private static void Main()
		{
			Console.OutputEncoding = Encoding.Default;
			
			Console.WriteLine("start");

			IDbContext dbContext = new DbContext(StaticContainer.StringConnection);
			IPasswordHash passwordHashGenerator = new PasswordHash(UserAccess.PasswordHashLength);

			TestDb(dbContext, passwordHashGenerator);


			Console.WriteLine("finish!");
			Console.ReadLine();


		}

		private static void TestDb(IDbContext dbContext, IPasswordHash passwordHash)
		{

			


		}

		private static void TestUniqueLogin(IDbContext dbContext, IPasswordHash passwordHash)
		{
			
		}
	}
}
