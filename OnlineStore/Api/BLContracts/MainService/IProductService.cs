using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IProductService
	{
		(ServiceResult actionResult, Product product) GetById(int id);
		(ServiceResult actionResult, IEnumerable<Product> products) GetAll();
		(ServiceResult actionResult, IEnumerable<Product> products) Search(ProductSearchRequest searchRequest);
		(ServiceResult actionResult, IEnumerable<Product> products) GetByIdList(IEnumerable<int> idProducts);

		ServiceResult SaveNewProduct(string sessionToken, ProductDataRequest productData);
		ServiceResult UpdateProduct(string sessionToken, ProductDataRequest productData);
		ServiceResult DeleteProduct(string sessionToken, int id);
	}
}
