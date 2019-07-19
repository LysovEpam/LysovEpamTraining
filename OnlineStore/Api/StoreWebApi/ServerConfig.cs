namespace StoreWebApi
{
	public static class ServerConfig
	{
		public static string GetStringConnection()
		{
			string stringConnection = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=OnlineStore; Integrated Security=True";

			return stringConnection;
		}

	}
}
