using System;
using System.Collections.Generic;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserOrder : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int AddressMaxLength { get; } = 500;
		public static int AddressMinLength { get; } = 1;

		public static int StatusMaxLength { get; } = 50;
		public static int StatusMinLength { get; } = 1;


		#endregion

		#region Свойства класса

		private string _address;
		private string _status;
		private int _userId;

		private OrderStatus _orderStatus;
		private UserSystem _userSystem;


		

		public string Address
		{
			get => _address;
			set
			{
				(bool, string) validParameter = ValidAddress(value);

				if (!validParameter.Item1)
					throw new ArgumentException(validParameter.Item2, $"{nameof(Address)}");

				_address = value;
			}
		}
		public string Status
		{
			get => _status;
			set
			{
				(bool, string) validParameter = ValidStatus(value);

				if (!validParameter.Item1)
					throw new ArgumentException(validParameter.Item2, $"{nameof(Status)}");

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

				if(UserSystem?.IdEntity != null && UserSystem.IdEntity.Value!= value)
					_userSystem = null;

				_userId = value;
				
			}
		}
		public DateTime DateOrder { get; set; }
		public List<Product> Products { get; set; }



		public OrderStatus OrderStatus
		{
			get => _orderStatus;
			set
			{
				if (value != null)
				{
					_orderStatus = value;
					_status = value.GetStatusName();
				}
				else
				{
					throw new ArgumentException($"{nameof(OrderStatus)} must not be empty",
						$"{nameof(OrderStatus)}");
				}
			}
		}
		public UserSystem UserSystem
		{
			get => _userSystem;
			set
			{
				_userSystem = value;

				if (value?.IdEntity != null && value.IdEntity.Value != UserId)
					_userId = value.IdEntity.Value;

			}
		}


		#endregion

		#region Конструктор

		public UserOrder()
		{

		}
		public UserOrder(int orderId, DateTime dateOrder, string address, string orderStatus, int idUser, List<Product> products) 
			: base(orderId)
		{
			DateOrder = dateOrder;
			Address = address;
			Status = orderStatus;

			UserSystem = null;
			UserId = idUser;
			
			Products = products;

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
			Status = orderStatus;
			UserSystem = userSystem;
			Products = products;
		}
		public UserOrder(DateTime dateOrder, string address, OrderStatus orderStatus, UserSystem userSystem, List<Product> products)
		{
			DateOrder = dateOrder;
			Address = address;
			OrderStatus = orderStatus;
			UserSystem = userSystem;
			Products = products;
		}

		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidStatus(string status)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(status))
				result = (false, $"{nameof(Status)} must not be empty");
			else if (status.Length > StatusMaxLength)
				result = (false, $"{nameof(Status)}  must not exceed {StatusMaxLength} characters");

			return result;
		}
		public static (bool, string) ValidAddress(string address)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(address))
				result = (false, $"{nameof(Address)} must not be empty");
			else if (address.Length > AddressMaxLength)
				result = (false, $"{nameof(Address)}  must not exceed {AddressMaxLength} characters");

			return result;
		}

		#endregion
	}
}
