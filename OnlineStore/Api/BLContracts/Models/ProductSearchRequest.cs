using System.Collections.Generic;

namespace BLContracts.Models
{
	public class ProductSearchRequest
	{
		public decimal MinCost { get; set; }
		public decimal MaxCost { get; set; }
		public string ProductSearch { get; set; }
		public IEnumerable<string> ProductStatuses { get; set; }
		public IEnumerable<int> IdProductCategories { get; set; }
	}
}
