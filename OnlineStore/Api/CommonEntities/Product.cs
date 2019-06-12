using System;
using CommonEntities.Additional;

namespace CommonEntities
{
	public class Product : BaseDbEntity
	{
		#region Статические свойства аргументов класса

		public static int ProductStatusMaxLength { get; } = 50;
		public static decimal ProductPriceMin { get; } = 0;

		#endregion

		#region Свойства класса


		private decimal _price;
		private string _status;
		private int _idProductInformation;

		private ProductStatus _productStatus;
		private ProductInformation _productInformation;


		public decimal Price
		{
			get => _price;
			set
			{
				(bool, string) validParameter = ValidPrice(value);
				if (!validParameter.Item1)
					throw new ArgumentException(validParameter.Item2, $"{nameof(Price)}");

				_price = value;

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
			}
		}
		public int IdProductInformation
		{
			get => _idProductInformation;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"{nameof(IdProductInformation)} mast be more zero",
						$"{nameof(IdProductInformation)}");

				_idProductInformation = value;

				if (_productInformation?.IdEntity != null && _productInformation.IdEntity.Value != value)
					_productInformation = null;
			}
		}

		public ProductStatus ProductStatus
		{
			get => _productStatus;
			set
			{
				if (value != null)
				{
					_productStatus = value;
					_status = value.GetStatusName();
				}
				else
				{
					throw new ArgumentException($"{nameof(ProductStatus)} must not be empty",
						$"{nameof(ProductStatus)}");
				}
			}
		}
		public ProductInformation ProductInformation
		{
			get => _productInformation;
			set
			{
				_productInformation = value;

				if (value?.IdEntity != null && value.IdEntity.Value != IdProductInformation)
					_idProductInformation = value.IdEntity.Value;
			}
		}

		
		#endregion

		#region Конструктор

		public Product()
		{

		}

		public Product(int id, decimal price, string status, int idProductInformation):base(id)
		{
			Price = price;
			Status = status;
			IdProductInformation = idProductInformation;
		}
		public Product(int id, decimal price, string status, ProductInformation productInformation) : base(id)
		{
			Price = price;
			Status = status;
			ProductInformation = productInformation;
		}
		public Product(int id, decimal price, ProductStatus status, ProductInformation productInformation) : base(id)
		{
			Price = price;
			ProductStatus = status;
			ProductInformation = productInformation;
		}
		public Product(int id, decimal price, ProductStatus status, int idProductInformation) : base(id)
		{
			Price = price;
			ProductStatus = status;
			IdProductInformation = idProductInformation;
		}

		public Product(decimal price, ProductStatus status, ProductInformation productInformation)
		{
			Price = price;
			ProductStatus = status;
			ProductInformation = productInformation;
		}
		public Product(decimal price, ProductStatus status, int idProductInformation)
		{
			Price = price;
			ProductStatus = status;
			IdProductInformation = idProductInformation;
		}


		#endregion

		#region Проверка корректности полей модели

		public static (bool, string) ValidStatus(string productStatus)
		{
			(bool, string) result = (true, null);

			if (string.IsNullOrEmpty(productStatus))
				result = (false, $"{nameof(ProductStatus)} must not be empty");
			else if (productStatus.Length > ProductStatusMaxLength)
				result = (false, $"{nameof(ProductStatus)}  must not exceed {ProductStatusMaxLength} characters");

			return result;
		}
		public static (bool, string) ValidPrice(decimal productPrice)
		{
			(bool, string) result = (true, null);

			if (productPrice < ProductPriceMin)
				result = (false, $"{nameof(Price)} mast be more {ProductPriceMin}");

			return result;
		}

		#endregion
	}
}
