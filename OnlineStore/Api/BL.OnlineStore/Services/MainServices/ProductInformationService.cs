using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.MainService;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.Services.MainServices
{
	public class ProductInformationService :  IProductInformationService
	{
		private readonly IDbContext _dbContext;

		public ProductInformationService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		
		public (ServiceResult, ProductInformation) GetById(int id)
		{
			var productList = _dbContext.ProductInformations.SelectById(id);

			ServiceResult actionResult;

			if (productList == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return (actionResult, productList);
		}
		public (ServiceResult, List<ProductInformation>) GetAll()
		{
			var list = _dbContext.ProductInformations.SelectAll();

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}
		public (ServiceResult actionResult, List<ProductInformation> productInformations) SearchInformation(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				ServiceResult actionResultError =
					new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
						"Search string cannot be empty");

				return (actionResultError, null);
			}

			var list = _dbContext.ProductInformations.Find(c =>
				c.ProductName == searchString || c.Description == searchString);

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return (actionResult, list);


		}



		public ServiceResult SaveNewProductInformation(string sessionToken, ProductInformation productInformation)
		{
			var saveResult = _dbContext.ProductInformations.Insert(productInformation);

			if (!saveResult.HasValue)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to save new product information");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}
		public ServiceResult UpdateProductInformation(string sessionToken, ProductInformation productInformation)
		{
			var updateResult = _dbContext.ProductInformations.Update(productInformation);

			if (!updateResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to update product information");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}
		public ServiceResult DeleteProductInformation(string sessionToken, int id)
		{
			var checkDelete = _dbContext.ProductInformations.GetCountDependencies(id);

			if (checkDelete > 0)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Product information cannot be deleted because it is associated with product.");

			var deleteResult = _dbContext.ProductInformations.Delete(id);

			if (!deleteResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to delete product information");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}

		

		

		
	}
}
