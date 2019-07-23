using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities;

namespace BLContracts.Services
{
	public interface IUserOrderService
	{
		
		(ServiceResult actionResult, OrderData userOrder) GetById(int id, string sessionToken);
		(ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetAll();
		(ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetByUser(string sessionToken);
		(ServiceResult actionResult, IEnumerable<OrderData> userOrders) GetBySearch(OrderSearchRequest searchRequest);

		ServiceResult SaveOrder(OrderData orderRequest);
		ServiceResult UpdateOrder(OrderData orderRequest);
		ServiceResult DeleteOrder(int id);


	}
}
