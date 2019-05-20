using System;

namespace CommonEntities.Additional
{
	public class UserStatus
	{
		public enum StatusEnum
		{
			Active,
			Block,
			Delete
		}

		public StatusEnum Status { get; }

		public UserStatus(string status)
		{
			Status = GetStatus(status);
		}
		public UserStatus(StatusEnum status)
		{
			Status = status;
		}


		public string GetStatusName()
		{
			return GetStatusName(Status);
		}
		

		#region Статические методы получения статуса

		public static string GetStatusName(StatusEnum status)
		{
			switch (status)
			{
				case StatusEnum.Active: return "User is active";
				case StatusEnum.Block: return "User is block";
				case StatusEnum.Delete: return "User is delete";
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static StatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case "User is active": return StatusEnum.Active;
				case "User is block": return StatusEnum.Block;
				case "User is delete": return StatusEnum.Delete;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		
		#endregion


	}


}
