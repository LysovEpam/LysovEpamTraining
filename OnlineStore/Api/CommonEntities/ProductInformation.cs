using System.Collections.Generic;

namespace CommonEntities
{
	public class ProductInformation : BaseDbEntity
	{
		public string ProductName { get; set; }
		public string ImageLocalSource { get; set; }
		public string Description { get; set; }
		public List<ProductCategory> ProductCategories { get; set; }

		#region Конструктор

		public ProductInformation()
		{

		}
		
		public ProductInformation(string productName, string imageLocalSource, string description, 
			List<ProductCategory> productCategories)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			ProductCategories = productCategories;
		}
		public ProductInformation(int productListId, string productName, string imageLocalSource, string description, 
			List<ProductCategory> productCategories) : base(productListId)
		{
			ProductName = productName;
			ImageLocalSource = imageLocalSource;
			Description = description;
			ProductCategories = productCategories;
		}

		#endregion
		
	}
}
