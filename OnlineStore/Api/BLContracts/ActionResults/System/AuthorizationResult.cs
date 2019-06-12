using System;
using CommonEntities.Additional;

namespace BLContracts.ActionResults.System
{
	public class AuthorizationResult : BaseActionResult
	{
		public string SessionToken { get;}
		
		public AuthorizationResult(ResultConnectionEnum resultConnection, string message, string sessionToken) : base(resultConnection, message)
		{
			SessionToken = sessionToken;
		}

		public AuthorizationResult(BaseActionResult baseResult, string sessionToken):base(baseResult.ResultConnection, baseResult.Message)
		{
			SessionToken = sessionToken;
		} 
	}
}
