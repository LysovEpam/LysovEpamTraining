using System.Collections.Generic;

namespace BLContracts.Models
{
	public class ProductDataRequest
	{
		public int IdEntity { get; set; }
		public decimal Price { get; set; }
		public string Status { get; set; }
		public int IdProductInformation { get; set; }

	}
}
