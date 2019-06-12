using System.Collections.Generic;
using BLContracts.ActionResults;
using CommonEntities;

namespace BLContracts.MainBl
{
	public interface IUserOrderBlModel
	{
		
		(BaseActionResult, UserOrder) GetById(int id);
		(BaseActionResult, List<UserOrder>) GetAll();

		(BaseActionResult, List<UserOrder>) GetByUserId(int userId);
		(BaseActionResult, List<UserOrder>) GetByStatus(string orderStatus);
		(BaseActionResult, List<UserOrder>) GetBySearch(string searchString);

		BaseActionResult SaveReserveOrder(string sessionToken, UserOrder order);
		BaseActionResult SaveFinishOrder(string sessionToken, UserOrder order);

		
		BaseActionResult UpdateOrder(string sessionToken, UserOrder order);
		BaseActionResult DeleteOrder(string sessionToken, int id);


	}
}
