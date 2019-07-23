using System.Collections.Generic;
using System.Linq;
using BLContracts.ActionResults;
using BLContracts.Services;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.Services
{
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IDbContext _dbContext;

		public ProductCategoryService(IDbContext dbContext)
			
		{
			_dbContext = dbContext;
			
		}
		
		public (ServiceResult actionResult,  ProductCategory productCategory) GetById(int id)
		{
			var productCategory = _dbContext.ProductCategories.SelectById(id);

			ServiceResult actionResult;

			if (productCategory == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return (actionResult , productCategory);
		}
		public (ServiceResult actionResult, List<ProductCategory> productCategories) GetAll()
		{
			var list = _dbContext.ProductCategories.SelectAll();

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}
		public (ServiceResult actionResult, List<ProductCategory> productCategories) SearchCategory(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				ServiceResult actionResultError =
					new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
						"Search string cannot be empty");

				return (actionResultError, null);
			}

			var list = _dbContext.ProductCategories.Find(c =>
				c.CategoryName.Contains(searchString) || c.Description.Contains(searchString));

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);


		}


		public ServiceResult SaveNewCategory( ProductCategory productCategory)
		{
			var result = _dbContext.ProductCategories.Find(category => category.CategoryName == productCategory.CategoryName);
			if (result.Count != 0)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.InvalidRequestData, "Product category name is exist");

			var saveResult = _dbContext.ProductCategories.Insert(productCategory);

			if (!saveResult.HasValue)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to save product category");

			}

			ServiceResult actionResultCorrect = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;

		}
		public ServiceResult UpdateCategory(ProductCategory productCategory)
		{
			var result = _dbContext.ProductCategories.Find(category => category.CategoryName == productCategory.CategoryName);
			if (result.Count != 0)
			{
				// ReSharper disable once PossibleInvalidOperationException
				// ReSharper disable once PossibleNullReferenceException
				int idExistCategory = result.ToList().FirstOrDefault().IdEntity.Value;
				// ReSharper disable once PossibleInvalidOperationException
				int idUpdateCategory = productCategory.IdEntity.Value;

				if (idExistCategory != idUpdateCategory)
					return new ServiceResult(ServiceResult.ResultConnectionEnum.InvalidRequestData,
						"Product category with that name already exists");

			}
			
			var updateResult = _dbContext.ProductCategories.Update(productCategory);

			if (!updateResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to update product category");

			}
			
			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}
		public ServiceResult DeleteCategory(int id)
		{

			var checkDelete = _dbContext.ProductCategories.GetCountDependencies(id);

			if(checkDelete >0)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Product category cannot be deleted because it is associated with product information.");


			var deleteResult = _dbContext.ProductCategories.Delete(id);

			if (!deleteResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to delete product category");
			}


			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}


		


	}
}
