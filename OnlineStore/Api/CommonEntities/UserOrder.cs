using System;
using System.Collections.Generic;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserOrder : BaseDbEntity
	{


		public string Address { get; set; }
		public DateTime DateOrder { get; set; }
		public int UserId { get; set; }
		public List<Product> Products { get; set; }



		public OrderStatus OrderStatus { get; set; }
		public UserSystem UserSystem { get; set; }


		#region Конструктор

		public UserOrder()
		{

		}
		public UserOrder(int orderId, DateTime dateOrder, string address, string orderStatus, int idUser, List<Product> products)
			: base(orderId)
		{
			Address = address;
			OrderStatus = new OrderStatus(orderStatus);
			DateOrder = dateOrder;
			UserId = idUser;
			Products = products;
			UserSystem = null;

		}

		public UserOrder( DateTime dateOrder, string address, string orderStatus, int idUser, List<Product> products)
		{
			Address = address;
			OrderStatus = new OrderStatus(orderStatus);
			DateOrder = dateOrder;
			UserId = idUser;
			Products = products;
			UserSystem = null;

		}

		public UserOrder(int orderId, DateTime dateOrder, string address, OrderStatus orderStatus, UserSystem userSystem, List<Product> products)
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			OrderStatus = orderStatus;
			UserSystem = userSystem;
			Products = products;
		}
		public UserOrder(int orderId, DateTime dateOrder, string address, string orderStatus, UserSystem userSystem, List<Product> products)
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			OrderStatus = new OrderStatus(orderStatus);
			UserSystem = userSystem;
			Products = products;
		}
		

		#endregion


	}
}
