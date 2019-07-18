namespace BLContracts.ActionResults
{
	public class ServiceResult
	{
		public enum ResultConnectionEnum
		{
			Correct = 0,
			AccessDenied = 1,
			NeedReAuthorization = 2,
			SystemError = 3,
			InvalidRequest = 4,
			InvalidRequestData = 5 
		}


		public ResultConnectionEnum ResultConnection { get; set; }
		public string Message { get; set; }

		public ServiceResult(ResultConnectionEnum resultConnection, string message)
		{
			ResultConnection = resultConnection;
			Message = message;
		}
	}
}
