using System;

namespace CommonEntities.Additional
{
	public class AuthorizationStatus
	{
		public enum AuthorizationStatusEnum
		{
			Active,
			BlockTimeOut,
			BlockNewAuthorization,
			BlockSystem
		}

		public AuthorizationStatusEnum Status { get; }

		public AuthorizationStatus(string status)
		{
			Status = GetStatus(status);
		}
		public AuthorizationStatus(AuthorizationStatusEnum status)
		{
			Status = status;
		}


		public string GetStatusName()
		{
			return GetStatusName(Status);
		}
		


		#region Статические методы получения статуса

		public static string GetStatusName(AuthorizationStatusEnum status)
		{
			switch (status)
			{
				case AuthorizationStatusEnum.Active: return "Active";
				case AuthorizationStatusEnum.BlockTimeOut: return "Block time out";
				case AuthorizationStatusEnum.BlockNewAuthorization: return "Block new authorization";
				case AuthorizationStatusEnum.BlockSystem: return "Block system";
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static AuthorizationStatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case "Active": return AuthorizationStatusEnum.Active;
				case "Block time out": return AuthorizationStatusEnum.BlockTimeOut;
				case "Block new authorization": return AuthorizationStatusEnum.BlockNewAuthorization;
				case "Block system": return AuthorizationStatusEnum.BlockSystem;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		

		#endregion


	}
}
