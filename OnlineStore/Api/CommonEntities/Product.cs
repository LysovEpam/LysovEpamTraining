using CommonEntities.Additional;

namespace CommonEntities
{
	public class Product : BaseDbEntity
	{

		public decimal Price { get; set; }
		//public string Status { get; set; }
		public int IdProductInformation { get; set; }

		public ProductStatus ProductStatus { get; set; }
		public ProductInformation ProductInformation { get; set; }

		#region Конструктор

		public Product()
		{

		}

		public Product(int id, decimal price, string status, int idProductInformation) : base(id)
		{
			Price = price;
			
			IdProductInformation = idProductInformation;
			ProductStatus = new ProductStatus(status);
			ProductInformation = null;
		}
		public Product(decimal price, string status, int idProductInformation)
		{
			Price = price;
			IdProductInformation = idProductInformation;
			ProductStatus = new ProductStatus(status);
			ProductInformation = null;
		}


		public Product(decimal price, ProductStatus status, ProductInformation productInformation)
		{
			Price = price;
			//Status = status.GetStatusName();
			ProductInformation = productInformation;
			if (productInformation?.IdEntity != null)
				IdProductInformation = productInformation.IdEntity.Value;
			ProductStatus = status;
		}

		#endregion

	}
}
