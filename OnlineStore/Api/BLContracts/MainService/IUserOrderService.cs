using System.Collections.Generic;
using BLContracts.ActionResults;
using BLContracts.Models;
using CommonEntities;

namespace BLContracts.MainService
{
	public interface IUserOrderService
	{
		
		(ServiceResult actionResult, UserOrder userOrder) GetById(string sessionToken, int id);
		(ServiceResult actionResult, IEnumerable<UserOrder> userOrders) GetAll(string sessionToken);
		(ServiceResult actionResult, IEnumerable<UserOrder> userOrders) GetByUser(string sessionToken, string userLogin);
		(ServiceResult actionResult, IEnumerable<UserOrder>) GetBySearch(string sessionToken, OrderSearchRequest searchRequest);

		ServiceResult SaveOrder(string sessionToken, OrderRequest orderRequest);
		ServiceResult UpdateOrder(string sessionToken, OrderRequest orderRequest);
		ServiceResult DeleteOrder(string sessionToken, int id);


	}
}
