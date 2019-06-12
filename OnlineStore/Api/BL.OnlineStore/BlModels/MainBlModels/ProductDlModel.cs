using System.Collections.Generic;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class ProductDlModel : BaseBlModel, IProductBlModel
	{
		private readonly IDbContext _dbContext;

		public ProductDlModel(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			_dbContext.RepositoryEvent += DbRepositoryEvent;
		}
		private void DbRepositoryEvent(string location, string caption, string description)
		{
			SaveEvent(TypeEvent.DbError, location, caption, description);
		}


		public (BaseActionResult, Product) GetById(int id)
		{
			var product = _dbContext.Products.SelectById(id);

			BaseActionResult actionResult;

			if (product == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return (actionResult, product);
		}

		public (BaseActionResult, List<Product>) GetAll()
		{
			var list = _dbContext.Products.SelectAll();

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}
		public (BaseActionResult, List<Product>) GetByStatus(string productStatus)
		{
			if (productStatus == null)
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "Статус не может быть пустым");

				return (actionResultError, null);
			}

			var list = _dbContext.Products.Find(c => c.Status == productStatus);

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}
		public (BaseActionResult, List<Product>) GetBySearch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
						"Строка поиска не может быть пустой");

				return (actionResultError, null);
			}

			var list = _dbContext.Products.Find(c =>
				c.Status.Contains(searchString)||
				c.ProductInformation.Description.Contains(searchString) ||
				c.ProductInformation.ProductName.Contains(searchString));

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}


		public BaseActionResult SaveNewProduct(string sessionToken, Product product)
		{
			var saveResult = _dbContext.Products.Insert(product);

			if (!saveResult.HasValue)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось сохранить новый продукт");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductDlModel)} - {nameof(SaveNewProduct)}",
					"Ошибка при сохранении нового продукта",
					"Не удалось сохранить новый продукт");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public BaseActionResult UpdateProduct(string sessionToken, Product product)
		{
			var saveResult = _dbContext.Products.Update(product);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось изменить информацию о продукте");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductDlModel)} - {nameof(UpdateProduct)}",
					"Ошибка при обновлении информации о продукте",
					"Не удалось изменить информацию о продукте");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public BaseActionResult DeleteProduct(string sessionToken, int id)
		{
			var saveResult = _dbContext.Products.Delete(id);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось удалить продукт");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductDlModel)} - {nameof(DeleteProduct)}",
					"Ошибка при удалении продукта",
					"Не удалось удалить продукт");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		

		

	

		
	}
}
