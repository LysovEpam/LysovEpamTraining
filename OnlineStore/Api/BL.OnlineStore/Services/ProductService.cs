using System.Collections.Generic;
using System.Linq;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services
{
	public class ProductService :  IProductService
	{
		private readonly IDbContext _dbContext;

		public ProductService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public (ServiceResult, Product) GetById(int id)
		{
			var product = _dbContext.Products.SelectById(id);

			ServiceResult actionResult;

			if (product == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return (actionResult, product);
		}
		public (ServiceResult, IEnumerable<Product>) GetAll()
		{
			var list = _dbContext.Products.SelectAll();

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}

		public (ServiceResult actionResult, IEnumerable<Product> products) Search(ProductSearchRequest searchRequest)
		{
			var allList = _dbContext.Products.SelectAll();

			if (allList == null)
				return (new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, ""), null);

			
			bool searchStatus;
			bool searchCost;
			bool searchParameter;
			bool searchCategory;

			List<ProductStatus> productStatuses = new List<ProductStatus>();
			
			#region Search parameters init

			try
			{
				foreach (string productStatus in searchRequest.ProductStatuses)
				{
					var status = new ProductStatus(productStatus);
					productStatuses.Add(status);
				}

				searchStatus = true;
			}
			catch
			{
				productStatuses = null;
				searchStatus = false;
			}

			if(searchRequest.MinCost != 0 || searchRequest.MaxCost != 0)
				searchCost = true;
			else
				searchCost = false;

			if (!string.IsNullOrEmpty(searchRequest.ProductSearch))
				searchParameter = true;
			else
				searchParameter = false;


			if (searchRequest.IdProductCategories != null && searchRequest.IdProductCategories.Count() != 0)
				searchCategory = true;
			else
				searchCategory = false;

			#endregion

			#region Search in the list

			
			var listResult = new List<Product>();

			foreach (Product product in allList)
			{

				#region Status check


				if (searchStatus)
				{
					bool statusCheck = false;

					foreach (ProductStatus productStatus in productStatuses)
					{
						if (product.ProductStatus.Status == productStatus.Status)
							statusCheck = true;
					}

					if(!statusCheck)
						continue;
					
				}

				#endregion

				#region Price check

				if (searchCost)
					if (product.Price > searchRequest.MaxCost || product.Price < searchRequest.MinCost)
						continue;

				#endregion

				#region Search string check

				if (searchParameter)
					if (!product.ProductInformation.ProductName.ToLower().Contains(searchRequest.ProductSearch.ToLower()) &&
					    !product.ProductInformation.Description.ToLower().Contains(searchRequest.ProductSearch.ToLower()))
						continue;

				#endregion

				#region Product category check

				
				if (searchCategory)
				{
					bool containCategories = true;

					foreach (int idSearchCategory in searchRequest.IdProductCategories)
					{
						bool containCategory = false;
						foreach (ProductCategory productCategory in product.ProductInformation.ProductCategories)
						{
							if (productCategory.IdEntity.Value == idSearchCategory)
								containCategory = true;
						}

						if (!containCategory)
						{
							containCategories = false;
							break;
						}

					}

					if(!containCategories)
						continue;
					
				}

				#endregion

				listResult.Add(product);
			}

			#endregion

			ServiceResult resultCorrect = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (resultCorrect, listResult);

		}
		public (ServiceResult actionResult, IEnumerable<Product> products) GetByIdList(IEnumerable<int> idProducts)
		{
			List<Product> listResult = new List<Product>();

			foreach (int idProduct in idProducts)
			{
				var product = _dbContext.Products.SelectById(idProduct);
				if (product != null)
					listResult.Add(product);
			}

			ServiceResult actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (actionResult, listResult);
		}


		public ServiceResult SaveNewProduct(ProductDataRequest productData)
		{

			var product = new Product(productData.Price, productData.Status, productData.IdProductInformation);



			var saveResult = _dbContext.Products.Insert(product);

			if (!saveResult.HasValue)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to save new product");

			}

			ServiceResult actionResultCorrect = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public ServiceResult UpdateProduct(ProductDataRequest productData)
		{
			var product = new Product(productData.IdEntity, productData.Price, productData.Status, productData.IdProductInformation);

			var updateResult = _dbContext.Products.Update(product);

			if (!updateResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to update product");

			}

			ServiceResult actionResultCorrect = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public ServiceResult DeleteProduct(int id)
		{
			var checkDelete = _dbContext.Products.GetCountDependencies(id);

			if (checkDelete > 0)
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Product cannot be deleted because it is associated with order.");

			var deleteResult = _dbContext.Products.Delete(id);

			if (!deleteResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "Failed to delete product");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

		}

		
	}
}
