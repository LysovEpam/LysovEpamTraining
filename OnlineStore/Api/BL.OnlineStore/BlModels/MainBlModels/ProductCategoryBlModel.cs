using System.Collections.Generic;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class ProductCategoryBlModel : BaseBlModel, IProductCategoryBlModel
	{
		private readonly IDbContext _dbContext;

		public ProductCategoryBlModel(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			_dbContext.RepositoryEvent += DbRepositoryEvent;
		}

		private void DbRepositoryEvent(string location, string caption, string description)
		{
			SaveEvent(TypeEvent.DbError, location, caption, description);
		}



		public (BaseActionResult, ProductCategory) GetById(int id)
		{
			var productCategory = _dbContext.ProductCategories.SelectById(id);

			BaseActionResult actionResult;

			if (productCategory == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return (actionResult, productCategory);
		}
		public (BaseActionResult, List<ProductCategory>) GetAll()
		{
			var list = _dbContext.ProductCategories.SelectAll();

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}

		public (BaseActionResult, List<ProductCategory>) GetByName(string categoryName)
		{
			if (string.IsNullOrEmpty(categoryName))
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "Категория не может быть пустой");

				return (actionResultError, null);
			}

			var list = _dbContext.ProductCategories.Find(c => c.CategoryName == categoryName);

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);
		}

		public (BaseActionResult, List<ProductCategory>) SearchCategory(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
						"Строка поиска не может быть пустой");

				return (actionResultError, null);
			}

			var list = _dbContext.ProductCategories.Find(c =>
				c.CategoryName.Contains(searchString) || c.Description.Contains(searchString));

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
			return (actionResult, list);


		}

		public BaseActionResult SaveNewCategory(string sessionToken, ProductCategory productCategory)
		{
			#region Проверка прав

			//var userToken = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			//if (userToken.AuthorizationStatus.Status != AuthorizationStatus.AuthorizationStatusEnum.Active||
			//    userToken.FinishSession < DateTime.Now)
			//{
			//	_programLogRegister.SaveEvent(TypeEvent.UserActionConnection,
			//		$"{nameof(ProductCategoryBlModel)} - {nameof(SaveNewCategory)}",
			//		"Ошибка при сохранении новой категории продукта",
			//		"Ошибка доступа, неактивный ключ сессии");

			//	return new BaseActionResult(BaseActionResult.ResultConnectionEnum.NeedReAuthorization,
			//		"Необходима повторная авторизация");
			//}

			//if (userToken.User.UserAccess.UserRole.Role != UserRole.RoleEnum.Admin &&
			//    userToken.User.UserAccess.UserRole.Role != UserRole.RoleEnum.Editor)
			//{
			//	_programLogRegister.SaveEvent(TypeEvent.UserActionConnection,
			//		$"{nameof(ProductCategoryBlModel)} - {nameof(SaveNewCategory)}",
			//		"Ошибка при сохранении новой категории продукта",
			//		"Ошибка доступа, авторизованный ");

			//	return new BaseActionResult(BaseActionResult.ResultConnectionEnum.AccessDenied,
			//		"Нет прав на добавление новой категории");
			//}

			#endregion

			var saveResult = _dbContext.ProductCategories.Insert(productCategory);

			if (!saveResult.HasValue)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось сохранить категорию товара");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductCategoryBlModel)} - {nameof(SaveNewCategory)}",
					"Ошибка при сохранении новой категории продукта",
					"Не удалось сохранить новую категорию товара");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;

		}
		public BaseActionResult UpdateCategory(string sessionToken, ProductCategory productCategory)
		{
			var saveResult = _dbContext.ProductCategories.Update(productCategory);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось изменить категорию товара");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductCategoryBlModel)} - {nameof(UpdateCategory)}",
					"Ошибка при обновлении категории продукта",
					"Не удалось изменить категорию товара");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public BaseActionResult DeleteCategory(string sessionToken, int id)
		{
			var saveResult = _dbContext.ProductCategories.Delete(id);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось удалить категорию товара");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(ProductCategoryBlModel)} - {nameof(DeleteCategory)}",
					"Ошибка при удалении категории продукта",
					"Не удалось удалить категорию товара");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}




	}
}
