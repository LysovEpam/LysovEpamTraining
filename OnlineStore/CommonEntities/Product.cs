using System;

namespace CommonEntities
{
	public class Product: BaseDbEntity
	{
		#region Свойства класса

		private decimal _price;
		private int _productListId;

		private ProductList _productList;

		
		public decimal Price
		{
			get => _price;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(Price)} mast be more zero", $"{nameof(Price)}");

				_price = value;
			}
		}
		public int ProductListId
		{
			get => _productListId;
			set
			{
				if (value <= 0)
					throw new ArgumentException($"Parameter {nameof(ProductListId)} mast be more zero", $"{nameof(ProductListId)}");

				_productListId = value;
			}
		}

		
		public ProductList ProductList
		{
			get => _productList;
			set
			{
				_productList = value;

				if (value?.IdEntity != null)
					ProductListId = value.IdEntity.Value;
				
			}
		}


		#endregion

		#region Конструктор

		public Product()
		{
			
		}

		public Product(int productId, decimal price, int productListId):base(productId)
		{
			Price = price;
			ProductListId = productListId;

			ProductList = null;
		}
		public Product(int productId, decimal price, ProductList productList) : base(productId)
		{
			Price = price;

			ProductList = productList;

			if(productList.IdEntity.HasValue)
				ProductListId = productList.IdEntity.Value;
		}
		public Product(decimal price, ProductList productList)
		{
			Price = price;

			ProductList = productList;

			if (productList.IdEntity.HasValue)
				ProductListId = productList.IdEntity.Value;
		}

		#endregion
	}
}
