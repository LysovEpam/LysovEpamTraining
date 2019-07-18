namespace CommonEntities
{
	public class ProductCategory : BaseDbEntity
	{
		
		public string CategoryName { get; set; }
		public string Description { get; set; }


		#region Конструктор

		public ProductCategory()
		{

		}
		public ProductCategory(string category, string description)
		{
			CategoryName = category;
			Description = description;
		}

		public ProductCategory(int id, string category, string description) : base(id)
		{
			CategoryName = category;
			Description = description;
		}

		#endregion

		
	}
}
