using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IProductCategoryBlModel
	{
		(BaseActionResult, ProductCategory) GetById(int id);
		(BaseActionResult, List<ProductCategory>) GetAll();
		(BaseActionResult, List<ProductCategory>) GetByName(string categoryName);
		(BaseActionResult, List<ProductCategory>) SearchCategory(string searchString);

		BaseActionResult SaveNewCategory(string sessionToken, ProductCategory productCategory);
		BaseActionResult UpdateCategory(string sessionToken, ProductCategory productCategory);
		BaseActionResult DeleteCategory(string sessionToken, int id);

	}
}
