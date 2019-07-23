using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.Services
{
	public interface IProductCategoryService
	{
		(ServiceResult actionResult, ProductCategory productCategory) GetById(int id);
		(ServiceResult actionResult, List<ProductCategory> productCategories) GetAll();
		(ServiceResult actionResult, List<ProductCategory> productCategories) SearchCategory(string searchString);

		ServiceResult SaveNewCategory(ProductCategory productCategory);
		ServiceResult UpdateCategory(ProductCategory productCategory);
		ServiceResult DeleteCategory( int id);

	}
}
