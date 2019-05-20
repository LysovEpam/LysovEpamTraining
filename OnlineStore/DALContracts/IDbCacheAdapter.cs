using CommonEntities;
using DALContracts.Repositories;

namespace DALContracts
{
	public interface IDbCacheAdapter : IDbContext
	{
		IRepositoryCache<ProductCategory> ProductCategoriesCache { get; }
		IRepositoryCache<ProductList> ProductListsCache { get; }
		IRepositoryCache<Product> ProductsCache { get; }
	}
}
