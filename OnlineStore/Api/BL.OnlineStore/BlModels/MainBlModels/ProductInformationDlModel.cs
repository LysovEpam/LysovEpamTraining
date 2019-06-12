using System;
using System.Collections.Generic;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class ProductInformationDlModel : BaseBlModel, IProductInformationBlModel
	{
		private readonly IDbContext _dbContext;

		public ProductInformationDlModel(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			_dbContext.RepositoryEvent += DbRepositoryEvent;
		}

		private void DbRepositoryEvent(string location, string caption, string description)
		{
			SaveEvent(TypeEvent.DbError, location, caption, description);
		}


		public (BaseActionResult, ProductInformation) GetById(int id)
		{
			var productList = _dbContext.ProductInformations.SelectById(id);

			BaseActionResult actionResult;

			if (productList == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return (actionResult, productList);
		}
		public (BaseActionResult, List<ProductInformation>) GetAll()
		{
			var list = _dbContext.ProductInformations.SelectAll();

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}
		public BaseActionResult SaveNewProductInformation(string sessionToken, ProductInformation productInformation)
		{
			var saveResult = _dbContext.ProductInformations.Insert(productInformation);

			if (!saveResult.HasValue)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось сохранить информацию о новом продукте");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductInformationDlModel)} - {nameof(SaveNewProductInformation)}",
					"Ошибка при сохранении информации о новом продукте",
					"Не удалось сохранить информацию о новом продукте");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}

		public BaseActionResult UpdateProductInformation(string sessionToken, ProductInformation productInformation)
		{
			var saveResult = _dbContext.ProductInformations.Update(productInformation);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось изменить информацию о продукте");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductInformationDlModel)} - {nameof(UpdateProductInformation)}",
					"Ошибка при обновлении информации о продукте",
					"Не удалось изменить информацию о продукте");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}

		public BaseActionResult DeleteProductInformation(string sessionToken, int id)
		{
			var saveResult = _dbContext.ProductCategories.Delete(id);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось удалить информацию о продукте");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductInformationDlModel)} - {nameof(DeleteProductInformation)}",
					"Ошибка при удалении информации о продукте",
					"Не удалось удалить информацию о продукте");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}

		

		

		
	}
}
