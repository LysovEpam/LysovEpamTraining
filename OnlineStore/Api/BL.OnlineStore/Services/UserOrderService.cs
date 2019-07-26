using System;
using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;

namespace BL.OnlineStore.Services
{
	public class UserOrderService : IUserOrderService
	{
		private readonly IDbContext _dbContext;

		public UserOrderService(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public (ServiceResult actionResult, OrderData userOrder) GetById(int id, string sessionToken)
		{
			var order = _dbContext.UserOrders.SelectById(id);

			var user = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			if (user.UserSystem.UserAdmittance.UserRole.Role != UserRole.RoleEnum.Admin &&
				user.UserId != order.UserId)
			{
				ServiceResult resultErrorAccess = new ServiceResult(ServiceResult.ResultConnectionEnum.AccessDenied,
					"Access to the order can be made by the user who made the order or administrator");
				return (resultErrorAccess, null);
			}

			if (order == null)
			{
				ServiceResult actionResultError = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "Order not found");
				return (actionResultError, null);
			}

			ServiceResult actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			order.UserSystem.UserAdmittance = null;

			var orderData = MapOrderData(order);

			return (actionResult, orderData);
		}

		public (ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetAll()
		{
			var list = _dbContext.UserOrders.SelectAll();

			ServiceResult actionResult;

			if (list == null)
			{
				ServiceResult actionResultError = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
				return (actionResultError, null);
			}
			else
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

				foreach (var userOrder in list)
					userOrder.UserSystem.UserAdmittance = null;

			}

			List<OrderData> listResult = new List<OrderData>();

			foreach (var order in list)
			{
				OrderData orderData = MapOrderData(order);
				listResult.Add(orderData);
			}

			listResult.Reverse();
			return (actionResult, listResult);
		}

		public (ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetByUser(string sessionToken)
		{
			var userInDb = _dbContext.UserAuthorizationsToken.GetByToken(sessionToken);

			var list = _dbContext.UserOrders.Find(c =>
				c.UserSystem.UserAdmittance.Login.ToLower() == userInDb.UserSystem.UserAdmittance.Login.ToLower());
			
			if (list == null)
			{
				ServiceResult actionResultError = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Not found");
				return (actionResultError, null);
			}
			
			ServiceResult actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			List<OrderData> listResult = new List<OrderData>();

			foreach (var order in list)
			{
				OrderData orderData = MapOrderData(order);
				listResult.Add(orderData);
			}

			listResult.Reverse();
			return (actionResult, listResult);
		}

		public (ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetBySearch(OrderSearchRequest searchRequest)
		{
			var list = _dbContext.UserOrders.SelectAll();

			List<OrderData> ordersResult = new List<OrderData>();

			OrderStatus orderStatus = new OrderStatus(searchRequest.Status);
			string searchString = searchRequest.SearchString.ToLower().Trim();

			foreach (var order in list)
			{
				if (order.OrderStatus.Status != orderStatus.Status)
					continue;

				if (order.IdEntity.HasValue && order.IdEntity.Value.ToString().Contains(searchString) ||
					order.Address.ToLower().Contains(searchString) ||
				    order.UserSystem.FirsName.ToLower().Contains(searchRequest.SearchString.ToLower()) ||
				    order.UserSystem.LastName.ToLower().Contains(searchRequest.SearchString.ToLower()) ||
				    order.UserSystem.Phone.ToLower().Contains(searchRequest.SearchString.ToLower()) ||
				    order.UserSystem.Email.ToLower().Contains(searchRequest.SearchString.ToLower()) )
				{
					OrderData orderData = MapOrderData(order);

					if(orderData!= null)
						ordersResult.Add(orderData);
				}

			}
			ordersResult.Reverse();
			ServiceResult serviceResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
			return (serviceResult, ordersResult);

		}

		public ServiceResult SaveOrder(OrderRequest orderRequest)
		{
			var products = new List<Product>();

			foreach (var productInList in orderRequest.IdProducts)
			{
				var product = _dbContext.Products.SelectById(productInList);
				if (product == null)
				{
					products = null;
					break;
				}

				if (product.ProductStatus.Status != ProductStatus.StatusEnum.Available &&
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

			var user = _dbContext.UsersSystem.GetUserByLogin(orderRequest.UserLogin);

			string orderStatus = new OrderStatus(OrderStatus.OrderStatusEnum.NewOrder).GetStatusName();
			DateTime dateOrder = DateTime.Now;

			// ReSharper disable once PossibleInvalidOperationException
			UserOrder userOrder = new UserOrder(dateOrder, orderRequest.Address, orderStatus, user.IdEntity.Value, products);

			var saveResult = _dbContext.UserOrders.Insert(userOrder);

			if (!saveResult.HasValue)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to save new order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

		}

		public ServiceResult UpdateOrder(OrderRequest orderRequest)
		{
			var products = new List<Product>();

			foreach (var idProduct in orderRequest.IdProducts)
			{
				var product = _dbContext.Products.SelectById(idProduct);
				if (product == null)
					break;
				
				products.Add(product);
			}

			if (products.Count == 0)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Could not change order. Error getting information about products.");
			}

			var oldOrder = _dbContext.UserOrders.SelectById(orderRequest.IdOrder);

			string orderStatus = new OrderStatus(orderRequest.Status).GetStatusName();

			
			UserOrder userOrder = new UserOrder(oldOrder.IdEntity.Value, oldOrder.DateOrder, orderRequest.Address, orderStatus, oldOrder.UserSystem.IdEntity.Value, products);

			var saveResult = _dbContext.UserOrders.Update(userOrder);

			if (!saveResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to update order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}
		public ServiceResult DeleteOrder(int id)
		{

			var deleteResult = _dbContext.UserOrders.Delete(id);

			if (!deleteResult)
			{
				return new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError,
					"Failed to delete order");
			}

			return new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
		}


		private OrderData MapOrderData(UserOrder order)
		{
			if (order == null)
				return null;

			SystemUserData systemUserData = new SystemUserData
			{
				Email = order.UserSystem.Email,
				Phone = order.UserSystem.Phone,
				FirstName = order.UserSystem.FirsName,
				LastName = order.UserSystem.LastName,
				IdUser = order.UserId
			};

			OrderData orderData = new OrderData
			{
				// ReSharper disable once PossibleInvalidOperationException
				IdEntity = order.IdEntity.Value,
				DateOrder = order.DateOrder,
				Address = order.Address,
				OrderStatus = order.OrderStatus,
				Products = order.Products,
				UserSystemData = systemUserData
			};

			return orderData;
		}





	}
}
