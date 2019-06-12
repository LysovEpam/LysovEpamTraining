namespace BLContracts.ActionResults
{
	public class BaseActionResult
	{
		public enum ResultConnectionEnum
		{
			Correct,
			AccessDenied,
			NeedReAuthorization,
			SystemError
		}


		public ResultConnectionEnum ResultConnection { get; set; }
		public string Message { get; set; }

		public BaseActionResult(ResultConnectionEnum resultConnection, string message)
		{
			ResultConnection = resultConnection;
			Message = message;
		}
	}
}
