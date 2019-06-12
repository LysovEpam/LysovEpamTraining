using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IProductInformationBlModel
	{
		(BaseActionResult, ProductInformation) GetById(int id);
		(BaseActionResult, List<ProductInformation>) GetAll();

		BaseActionResult SaveNewProductInformation(string sessionToken, ProductInformation productInformation);
		BaseActionResult UpdateProductInformation(string sessionToken, ProductInformation productInformation);
		BaseActionResult DeleteProductInformation(string sessionToken, int id);
	}
}
