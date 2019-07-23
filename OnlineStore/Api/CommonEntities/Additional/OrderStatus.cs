using System;


namespace CommonEntities.Additional
{
	public class OrderStatus
	{
		public const string StatusNewOrder = "NewOrder";
		public const string StatusProcessed = "Processed";
		public const string StatusPaid = "Paid";
		public const string StatusWaitingForDelivery = "WaitingForDelivery";
		public const string StatusCanceled = "Canceled";
		public const string StatusFulfilled = "Fulfilled";

		public enum OrderStatusEnum
		{
			NewOrder = 10,
			Processed = 11,
			Paid = 12,
			WaitingForDelivery =13,
			Canceled =14,
			Fulfilled = 15
		}


		public OrderStatusEnum Status { get; }

		public OrderStatus(string status)
		{
			Status = GetStatus(status);
		}
		public OrderStatus(OrderStatusEnum status)
		{
			Status = status;
		}


		public string GetStatusName()
		{
			return GetStatusName(Status);
		}


		public static string GetStatusName(OrderStatusEnum status)
		{
			switch (status)
			{
				case OrderStatusEnum.NewOrder: return StatusNewOrder;
				case OrderStatusEnum.Processed: return StatusProcessed;
				case OrderStatusEnum.Paid: return StatusPaid;
				case OrderStatusEnum.WaitingForDelivery: return StatusWaitingForDelivery;
				case OrderStatusEnum.Fulfilled: return StatusFulfilled;
				case OrderStatusEnum.Canceled: return StatusCanceled;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static OrderStatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case StatusNewOrder: return OrderStatusEnum.NewOrder;
				case StatusProcessed: return OrderStatusEnum.Processed;
				case StatusPaid: return OrderStatusEnum.Paid;
				case StatusWaitingForDelivery: return OrderStatusEnum.WaitingForDelivery;
				case StatusFulfilled: return OrderStatusEnum.Fulfilled;
				case StatusCanceled: return OrderStatusEnum.Canceled;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}


	}
}
