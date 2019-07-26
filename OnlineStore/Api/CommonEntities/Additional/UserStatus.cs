using System;

namespace CommonEntities.Additional
{
	public class UserStatus
	{

		public const string StatusActive = "Active";
		public const string StatusBlock = "Block";
		public const string StatusDelete = "Delete";
		

		public enum StatusEnum
		{
			Active =10,
			Block =11,
			Delete =12
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
		

		
		public static string GetStatusName(StatusEnum status)
		{
			switch (status)
			{
				case StatusEnum.Active: return StatusActive;
				case StatusEnum.Block: return StatusBlock;
				case StatusEnum.Delete: return StatusDelete;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static StatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case StatusActive: return StatusEnum.Active;
				case StatusBlock: return StatusEnum.Block;
				case StatusDelete: return StatusEnum.Delete;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		
		

	}


}
