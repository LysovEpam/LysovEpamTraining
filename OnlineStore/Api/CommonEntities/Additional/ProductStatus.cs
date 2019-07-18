using System;


namespace CommonEntities.Additional
{
	public class ProductStatus
	{
		public const string StatusAvailable = "Available";
		public const string StatusNeedToOrder = "NeedToOrder";
		public const string StatusNotAvailable = "NotAvailable";
		
		public enum StatusEnum
		{
			Available = 10,
			NeedToOrder = 11,
			NotAvailable = 12
		}

		public StatusEnum Status { get; }


		public ProductStatus(string status)
		{
			Status = GetStatus(status);
		}
		public ProductStatus(StatusEnum status)
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
				case StatusEnum.Available: return StatusAvailable;
				case StatusEnum.NeedToOrder: return StatusNeedToOrder;
				case StatusEnum.NotAvailable: return StatusNotAvailable;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static StatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case StatusAvailable: return StatusEnum.Available;
				case StatusNeedToOrder: return StatusEnum.NeedToOrder;
				case StatusNotAvailable: return StatusEnum.NotAvailable;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}

		
	}
}
