using System;
using CommonEntities.Additional;

namespace StoreWebApi.Models.ControllerResults
{
	public class AuthorizationResult
	{
		public string UserLogin { get; }
		public UserRole UserRole { get; }
		public string Jwt { get; }
		public DateTime DateTimeAuthorizationFinish { get; }

		public AuthorizationResult()
		{
			
		}

		public AuthorizationResult(string userLogin, UserRole userRole, string jwt, DateTime dateTimeAuthorizationFinish)
		{
			UserLogin = userLogin;
			UserRole = userRole;
			Jwt = jwt;
			DateTimeAuthorizationFinish = dateTimeAuthorizationFinish;
		}

		
	}
}
