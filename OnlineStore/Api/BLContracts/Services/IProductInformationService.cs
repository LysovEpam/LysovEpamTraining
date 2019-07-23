using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.Services
{
	public interface IProductInformationService
	{
		(ServiceResult actionResult, ProductInformation prouductInformation) GetById(int id);
		(ServiceResult actionResult, List<ProductInformation> productInformations) GetAll();
		(ServiceResult actionResult, List<ProductInformation> productInformations) SearchInformation(string searchString);

		ServiceResult SaveNewProductInformation(ProductInformation productInformation);
		ServiceResult UpdateProductInformation(ProductInformation productInformation);
		ServiceResult DeleteProductInformation(int id);
	}
}
