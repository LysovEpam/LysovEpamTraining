using System;
using System.Collections.Generic;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserOrder : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int AddressMaxLength { get; } = 500;

		#endregion

		#region Свойства класса

		private DateTime _dateOrder;
		private string _address;
		private string _status;
		private int _userId;

		private OrderStatus _orderStatus;
		private User _user;


		public DateTime DateOrder
		{
			get => _dateOrder;
			set
			{
				if (value > DateTime.Now)
					throw new ArgumentException("Order date can not be greater than current", $"{nameof(Address)}");


				_dateOrder = value;
			}
		}
		public string Address
		{
			get => _address;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException($"The parameter {nameof(Address)} must not be empty", $"{nameof(Address)}");
				if (value.Length > AddressMaxLength)
					throw new ArgumentException($"The parameter {nameof(Address)} must not exceed {AddressMaxLength} characters", $"{nameof(Address)}");

				_address = value;
			}
		}
		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				_orderStatus = new OrderStatus(value);
			}
		}
		public int UserId
		{
			get => _userId;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(UserId)} mast be more zero", $"{nameof(UserId)}");

				_userId = value;
				_user = null;
			}
		}

		public List<int> IdProducts { get; }
		public List<Product> Products { get; }


		public OrderStatus OrderStatus
		{
			get => _orderStatus;
			set
			{
				_orderStatus = value ??
					throw new ArgumentException($"The parameter {nameof(OrderStatus)} must not be empty",
						$"{nameof(OrderStatus)}");

				_status = value.GetStatusName();
			}
		}
		public User User
		{
			get => _user;
			set
			{
				_user = value;
				if (value?.IdEntity != null)
					_userId = value.IdEntity.Value;

			}
		}


		#endregion

		#region Конструктор

		public UserOrder()
		{

		}
		public UserOrder(int orderId, DateTime dateOrder, string address, string orderStatus, int idUser, List<int> idProducts)
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			Status = orderStatus;

			User = null;
			UserId = idUser;
			IdProducts = idProducts;
			Products = null;
		}

		public UserOrder(int orderId, DateTime dateOrder, string address, string orderStatus, int idUser, List<Product> products) 
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			Status = orderStatus;

			User = null;
			UserId = idUser;
			
			Products = products;

			IdProducts = new List<int>();

			foreach (Product product in products)
			{
				if (product.IdEntity != null)
					IdProducts.Add(product.IdEntity.Value);
			}
		}

		public UserOrder(int orderId, DateTime dateOrder, string address, OrderStatus orderStatus, User user, List<Product> products)
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			OrderStatus = orderStatus;

			User = user;
			if (user.IdEntity.HasValue)
				UserId = user.IdEntity.Value;

			Products = products;

			IdProducts = new List<int>();

			foreach (Product product in products)
			{
				if (product.IdEntity != null)
					IdProducts.Add(product.IdEntity.Value);
			}

		}

		public UserOrder(DateTime dateOrder, string address, OrderStatus orderStatus, User user, List<Product> products)
		{
			DateOrder = dateOrder;
			Address = address;
			OrderStatus = orderStatus;

			User = user;
			if (user.IdEntity.HasValue)
				UserId = user.IdEntity.Value;

			Products = products;

			IdProducts = new List<int>();

			foreach (Product product in products)
			{
				if (product.IdEntity != null)
					IdProducts.Add(product.IdEntity.Value);
			}

		}

		#endregion
	}
}
