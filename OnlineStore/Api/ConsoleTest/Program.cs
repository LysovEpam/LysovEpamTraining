using System;
using BL.OnlineStore;
using BL.OnlineStore.Services.MainServices;
using BL.OnlineStore.Services.SystemServices;
using BLContracts.Models;
using DAL.OnlineStore;
using DALContracts;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Start tests!!!");
			Console.WriteLine("====================================================================================================");


			Test();



			Console.WriteLine("====================================================================================================");
			Console.WriteLine("Finish tests!!!");
			Console.ReadLine();
		}

		static void Test()
		{
			string stringConnection = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=OnlineStore; Integrated Security=True";
			IDbContext dbContext = new DbContext(stringConnection);

			ProductCategoryService service = new ProductCategoryService(dbContext);

			var result = service.DeleteCategory("", 15);

			Console.WriteLine(result.Message);


		}
	}
}
