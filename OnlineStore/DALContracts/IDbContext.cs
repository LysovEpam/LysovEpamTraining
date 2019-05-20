using CommonEntities;
using DALContracts.Repositories;

namespace DALContracts
{
	public interface IDbContext
	{
		IRepositoryUserAccess UserAccesses { get; }
		IRepositoryUser Users { get; }
		IRepositoryUserAuthorization UserAuthorizations { get; }


		IRepository<ProductCategory> ProductCategories { get; }
		IRepository<ProductList> ProductLists { get; }
		IRepository<Product> Products { get; }


		IRepository<UserOrder> UserOrders { get; }

	}
}
