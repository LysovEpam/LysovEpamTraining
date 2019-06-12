using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IProductBlModel
	{
		(BaseActionResult, Product) GetById(int id);
		(BaseActionResult, List<Product>) GetAll();
		(BaseActionResult, List<Product>) GetByStatus(string productStatus);
		(BaseActionResult, List<Product>) GetBySearch(string searchString);


		BaseActionResult SaveNewProduct(string sessionToken, Product product);
		BaseActionResult UpdateProduct(string sessionToken, Product product);
		BaseActionResult DeleteProduct(string sessionToken, int id);
	}
}
