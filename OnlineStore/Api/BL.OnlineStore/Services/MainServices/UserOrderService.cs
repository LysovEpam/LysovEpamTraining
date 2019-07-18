using System;
using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.MainService;
using BLContracts.Models;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services.MainServices
{
	public class UserOrderService :  IUserOrderService
	{
		private readonly IDbContext _dbContext;

		public UserOrderService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public (ServiceResult actionResult, UserOrder userOrder) GetById(string sessionToken, int id)
		{
			var order = _dbContext.UserOrders.SelectById(id);

			ServiceResult actionResult;

			if (order == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
				order.UserSystem.UserAdmittance = null;
			}


			return (actionResult, order);
		}

		public (ServiceResult actionResult, IEnumerable<UserOrder> userOrders) GetAll(string sessionToken)
		{
			var list = _dbContext.UserOrders.SelectAll();

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAdmittance = null;

			}


			return (actionResult, list);
		}

		public (ServiceResult actionResult, IEnumerable<UserOrder> userOrders) GetByUser(string sessionToken, string userLogin)
		{
			var list = _dbContext.UserOrders.Find(c => c.UserSystem.UserAdmittance.Login == userLogin);

			ServiceResult actionResult;

			if (list == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAdmittance = null;

			}

			return (actionResult, list);
		}

		public (ServiceResult actionResult, IEnumerable<UserOrder>) GetBySearch(string sessionToken, OrderSearchRequest searchRequest)
		{
			throw new System.NotImplementedException();
		}

		public ServiceResult SaveOrder(string sessionToken, OrderRequest orderRequest)
		{
			var products = new List<Product>();
			
			foreach (int idProduct in orderRequest.IdProducts)
			{
				var product = _dbContext.Products.SelectById(idProduct);
				if (product == null)
				{
					products = null;
					break;
				}
				
				if(product.ProductStatus.Status != ProductStatus.StatusEnum.Available &&
				   product.ProductStatus.Status != ProductStatus.StatusEnum.NeedToOrder)
				{
					products = null;
					break;
				}

				products.Add(product);
			}

			if (products == null || products.Count == 0)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Unable to place an order. Some or all products are currently unavailable.");
			}


			var userByToken = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);
			if (userByToken.UserSystem.UserAdmittance.Login != orderRequest.UserLogin)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Unable to place an order. User authorization failed. Try logging in again.");
			}

			string orderStatus = new OrderStatus(OrderStatus.OrderStatusEnum.NewOrder).GetStatusName();
			DateTime dateOrder = DateTime.Now;

			UserOrder userOrder = new UserOrder(dateOrder, orderRequest.Address, orderStatus, userByToken.UserId, products);

			var saveResult = _dbContext.UserOrders.Insert(userOrder);

			if (!saveResult.HasValue)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to save new order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

		}

		public ServiceResult UpdateOrder(string sessionToken, OrderRequest orderRequest)
		{
			var products = new List<Product>();

			foreach (int idProduct in orderRequest.IdProducts)
			{
				var product = _dbContext.Products.SelectById(idProduct);
				if (product == null)
				{
					products = null;
					break;
				}
			}

			if (products == null || products.Count == 0)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Could not change order. Error getting information about products.");
			}

			var oldOrder = _dbContext.UserOrders.SelectById(orderRequest.IdOrder);

			string orderStatus = new OrderStatus(OrderStatus.OrderStatusEnum.NewOrder).GetStatusName();

			UserOrder userOrder = new UserOrder(oldOrder.DateOrder, orderRequest.Address, orderStatus, oldOrder.UserId, products);

			var saveResult = _dbContext.UserOrders.Update(userOrder);

			if (!saveResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to update order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}
		public ServiceResult DeleteOrder(string sessionToken, int id)
		{
			
			var deleteResult = _dbContext.UserOrders.Delete(id);

			if (!deleteResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to delete order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}




		

		
		
	}
}
