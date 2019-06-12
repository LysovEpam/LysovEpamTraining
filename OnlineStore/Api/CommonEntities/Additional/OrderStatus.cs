using System;


namespace CommonEntities.Additional
{
	public class OrderStatus
	{
		public enum OrderStatusEnum
		{
			NewOrder,	
			Processed,	
			Paid,      
			WaitingForDelivery,
			Delivered, 
			Fulfilled, 
			Canceled
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
		

		#region Статические методы получения статуса

		public static string GetStatusName(OrderStatusEnum status)
		{
			switch (status)
			{
				case OrderStatusEnum.NewOrder: return "New order";
				case OrderStatusEnum.Processed: return "Processed";
				case OrderStatusEnum.Paid: return "Paid";
				case OrderStatusEnum.WaitingForDelivery: return "Waiting for delivery";
				case OrderStatusEnum.Delivered: return "Delivered";
				case OrderStatusEnum.Fulfilled: return "Fulfilled";
				case OrderStatusEnum.Canceled: return "Canceled";
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		public static OrderStatusEnum GetStatus(string status)
		{
			switch (status)
			{
				case "New order": return OrderStatusEnum.NewOrder;
				case "Processed": return OrderStatusEnum.Processed;
				case "Paid": return OrderStatusEnum.Paid;
				case "Waiting for delivery": return OrderStatusEnum.WaitingForDelivery;
				case "Delivered": return OrderStatusEnum.Delivered;
				case "Fulfilled": return OrderStatusEnum.Fulfilled;
				case "Canceled": return OrderStatusEnum.Canceled;
				default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}
		}
		
		#endregion
	}
}
