using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IProductInformationService
	{
		(ServiceResult actionResult, ProductInformation prouductInformation) GetById(int id);
		(ServiceResult actionResult, List<ProductInformation> productInformations) GetAll();
		(ServiceResult actionResult, List<ProductInformation> productInformations) SearchInformation(string searchString);

		ServiceResult SaveNewProductInformation(string sessionToken, ProductInformation productInformation);
		ServiceResult UpdateProductInformation(string sessionToken, ProductInformation productInformation);
		ServiceResult DeleteProductInformation(string sessionToken, int id);
	}
}
