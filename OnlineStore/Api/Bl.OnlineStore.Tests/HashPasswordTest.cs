//using System;
//using BL.OnlineStore;
//using BLContracts;
//using CommonEntities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Bl.OnlineStore.Tests
//{
//	[TestClass]
//	public class HashPasswordTest
//	{

//		private static IPasswordHash _passwordHash;

//		[ClassInitialize]
//		public static void Init(TestContext testContext)
//		{
//			_passwordHash = new PasswordHash(UserAdmittance.PasswordHashLength);
//		}



//		[TestMethod]
//		public void GeneratePasswordHash_HashValue()
//		{
//			string login = "userRoot1";
//			string passwrod = "userRootTestPassword1";
//			string hash = "8nuhmNmMsBtq5Ye67lAUrD3ftwRqPotRog6YlvoKPW7WtVwAcV";

//			string hashTest = _passwordHash.GeneratePasswordHash(login, passwrod);

//			Assert.AreEqual(hash, hashTest);
//		}

//		[TestMethod]
//		public void GeneratePasswordHash_HashGenerator()
//		{
//			Random rand = new Random();
//			for (int i = 0; i < 1000; i++)
//			{
//				string seed = "ABCDEFGHIKLMNOPQRSTVXYZ1234567890";

//				int loginLength = rand.Next(UserAdmittance.LoginMinLength, UserAdmittance.LoginMaxLength);
//				int passwordLength = rand.Next(UserAdmittance.PasswordMinLength, UserAdmittance.PasswordMaxLength);

//				string login = "";
//				string password = "";

//				for (int j = 0; j < loginLength; j++)
//					login += seed[rand.Next(0, seed.Length)];

//				for (int j = 0; j < passwordLength; j++)
//					password += seed[rand.Next(0, seed.Length)];

//				string hash = _passwordHash.GeneratePasswordHash(login, password);

//				Assert.IsTrue(hash.Length== UserAdmittance.PasswordHashLength);

//			}

//		}




//	}
//}
