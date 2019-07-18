using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IProductCategoryService
	{
		(ServiceResult actionResult, ProductCategory productCategory) GetById(int id);
		(ServiceResult actionResult, List<ProductCategory> productCategories) GetAll();
		(ServiceResult actionResult, List<ProductCategory> productCategories) SearchCategory(string searchString);

		ServiceResult SaveNewCategory(string sessionToken, ProductCategory productCategory);
		ServiceResult UpdateCategory(string sessionToken, ProductCategory productCategory);
		ServiceResult DeleteCategory(string sessionToken, int id);

	}
}
