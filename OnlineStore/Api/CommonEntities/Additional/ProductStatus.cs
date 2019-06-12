using System;


namespace CommonEntities.Additional
{
	public class ProductStatus
	{
		public const string StatusAvailable = "Available";
		public const string StatusNotAvailable = "NotAvailable";
		public const string StatusReserved = "Reserved";
		public const string StatusSoldOut = "SoldOut";

		public enum StatusEnum
		{
			Available,
			NotAvailable,
			StatusReserved,
			SoldOut
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
		#region Статические методы получения статуса

		public static string GetStatusName(StatusEnum status)
		{
			switch (status)
			{
				case StatusEnum.Available: return StatusAvailable;
				case StatusEnum.NotAvailable: return StatusNotAvailable;
				case StatusEnum.StatusReserved: return StatusReserved;
				case StatusEnum.SoldOut: return StatusSoldOut;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static StatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case StatusAvailable: return StatusEnum.Available;
				case StatusNotAvailable: return StatusEnum.NotAvailable;
				case StatusReserved: return StatusEnum.StatusReserved;
				case StatusSoldOut: return StatusEnum.SoldOut;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}

		#endregion
	}
}
