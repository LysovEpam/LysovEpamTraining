using System.Collections.Generic;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class UserOrderBlModel : BaseBlModel, IUserOrderBlModel
	{
		private readonly IDbContext _dbContext;

		public UserOrderBlModel(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			_dbContext.RepositoryEvent += DbRepositoryEvent;
		}

		public (BaseActionResult, UserOrder) GetById(int id)
		{
			var order = _dbContext.UserOrders.SelectById(id);

			BaseActionResult actionResult;

			if (order == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
				order.UserSystem.UserAccess = null;
			}


			return (actionResult, order);
		}
		public (BaseActionResult, List<UserOrder>) GetAll()
		{
			var list = _dbContext.UserOrders.SelectAll();

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAccess = null;
				
			}

			
			return (actionResult, list);
		}
		public (BaseActionResult, List<UserOrder>) GetByStatus(string orderStatus)
		{
			if (orderStatus == null)
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "Статус не может быть пустым");

				return (actionResultError, null);
			}

			var list = _dbContext.UserOrders.Find(c => c.Status == orderStatus);

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAccess = null;

			}

			return (actionResult, list);
		}
		public (BaseActionResult, List<UserOrder>) GetBySearch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError,
						"Строка поиска не может быть пустой");

				return (actionResultError, null);
			}

			var list = _dbContext.UserOrders.Find(c =>
				c.Status.Contains(searchString) ||
				c.UserSystem.FirsName.Contains(searchString) ||
				c.UserSystem.LastName.Contains(searchString) ||
				c.UserSystem.Email.Contains(searchString) ||
				c.UserSystem.Phone.Contains(searchString));

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAccess = null;

			}


			return (actionResult, list);
		}
		public (BaseActionResult, List<UserOrder>) GetByUserId(int userId)
		{
			var list = _dbContext.UserOrders.Find(c => c.UserId == userId);

			BaseActionResult actionResult;

			if (list == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAccess = null;

			}

			return (actionResult, list);
		}

		public BaseActionResult SaveReserveOrder(string sessionToken, UserOrder order)
		{
			order.OrderStatus = new OrderStatus(OrderStatus.OrderStatusEnum.NewOrder);

			foreach (var product in order.Products)
			{
				// ReSharper disable once PossibleInvalidOperationException
				Product productDb = _dbContext.Products.SelectById(product.IdEntity.Value);

				if (productDb.ProductStatus.Status != ProductStatus.StatusEnum.Available)
				{
					BaseActionResult actionResultError = new BaseActionResult(
						BaseActionResult.ResultConnectionEnum.SystemError,
						"Не все продукты доступны для заказа!");

					return actionResultError;
				}
			}


			var saveResult = _dbContext.UserOrders.Insert(order);

			if (!saveResult.HasValue)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось сохранить новый заказ");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(UserOrderBlModel)} - {nameof(SaveReserveOrder)}",
					"Ошибка при сохранении нового заказа",
					"Не удалось сохранить новый заказ");

				return actionResultError;
			}

			foreach (var product in order.Products)
			{
				product.ProductStatus = new ProductStatus(ProductStatus.StatusEnum.StatusReserved);
				_dbContext.Products.Update(product);
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;


		}
		public BaseActionResult SaveFinishOrder(string sessionToken, UserOrder order)
		{
			throw new System.NotImplementedException();
		}

		
		public BaseActionResult UpdateOrder(string sessionToken, UserOrder order)
		{
			var saveResult = _dbContext.UserOrders.Update(order);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось изменить информацию о заказе");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(UserOrderBlModel)} - {nameof(UpdateOrder)}",
					"Ошибка при обновлении информации о заказе",
					"Не удалось изменить информацию о заказе");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}
		public BaseActionResult DeleteOrder(string sessionToken, int id)
		{
			var saveResult = _dbContext.UserOrders.Delete(id);

			if (!saveResult)
			{
				BaseActionResult actionResultError = new BaseActionResult(
					BaseActionResult.ResultConnectionEnum.SystemError,
					"Не удалось удалить заказ");

				SaveEvent(TypeEvent.UserActionConnection,
					$"{nameof(UserOrderBlModel)} - {nameof(DeleteOrder)}",
					"Ошибка при удалении заказа",
					"Не удалось удалить заказ");

				return actionResultError;
			}

			BaseActionResult actionResultCorrect = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			return actionResultCorrect;
		}


		private void DbRepositoryEvent(string location, string caption, string description)
		{
			SaveEvent(TypeEvent.DbError, location, caption, description);
		}

		
	}
}
