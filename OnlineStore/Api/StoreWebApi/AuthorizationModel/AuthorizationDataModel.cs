using System;

namespace StoreWebApi.AuthorizationModel
{
	public class AuthorizationDataModel
	{
		public const string SigningSecurityKey = "LqDyXwUpCucjqSaZDbQG7Z7NYS6Nhnvk8N79SgIMk3Ca0GPVVw";
		//"LnPcw7f3RbZ2UiM2lLKc1cC14zgdxAIkPTeYSUXP0XSRM1sLuEfmpPvsmgpjEFx5yres6ukd4YmyBVv7MBe9kfXoNJMBrfaY4wTG" +
		//"4QxQeRS0okJuXo4VZrw7sHC7wBsLLJABR6jGntbJYom60kK8UoQLvVq3EIC8EQFXCmY1JWtuh1BehgSV7zt4wbsGFatnT8KGArAn" +
		//"L1rpMLqGmyKcbQOKZN2vDsLeZMyS0XXQ7yYmD1QrLtnHxklNDYvWUOrV8D93u40emdttsbvOWXQvhWrjaJ54HsoslFlEQjjF3QLE" +
		//"ExVQXCLGE9RJMVQpACZyzFWGrPKjNOov5TrOHcg3LB5AdDUHBZNbxXtXymj5sU0qtch6uBj2zPpbiJTgRlJi4JhbDogZJ8rdY0HJ" +
		//"Hsd5Y5ArMbSvrCP6ZEvkBh4iN3pjMU7zyyhg5ncT5KcAC1gqe9FIuSy5wkHrY5Q0LFpjSCDya4tSno6ojjxYc1wHnKoNQzM7Z6fj";

		public const string ValidIssuer = "OnlineStore";
		public const string ValidAudience = "OnlineStoreClient";

		public const string JwtSchemeName = "JwtBearer";

		public static TimeSpan AuthorizationTtl { get; } = new TimeSpan(0,10,0,0);


		public const string ClaimLogin = "Login";
		public const string ClaimSessionToken = "SessionToken";


	}
}
