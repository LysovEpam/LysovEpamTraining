using CommonEntities;
using DALContracts.Repositories;

namespace DALContracts
{
	public interface IDbContext
	{
		IRepositoryUserAdmittance UserAdmittances { get; }
		IRepositoryUserSystem UsersSystem { get; }
		IRepositoryUserAuthorizationToken UserAuthorizationsToken { get; }


		IRepository<ProductCategory> ProductCategories { get; }
		IRepository<ProductInformation> ProductInformations { get; }
		IRepository<Product> Products { get; }


		IRepository<UserOrder> UserOrders { get; }

	}
}
