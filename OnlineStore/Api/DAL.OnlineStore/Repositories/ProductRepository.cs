using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using CommonEntities.Additional;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductRepository : ExecuteCommandBase, IRepository<Product>
	{
		#region Stored procedure names


		const string SpSelectById = @"Product_SelectById";
		const string SpSelectAll = @"Product_SelectAll";
		const string SpInsert = @"Product_Insert";
		const string SpUpdate = @"Product_Update";
		const string SpDelete = @"Product_Delete";
		const string SpGetDependencies = @"Product_CountDependencies";

		#endregion

		private readonly string _connectionString;
		private readonly ProductInformationRepository _productInformationRepository;

		public ProductRepository(string connectionString, ProductInformationRepository productInformationRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_productInformationRepository = productInformationRepository;
		}


		public Product SelectById(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(ProductRepository)}-{nameof(SelectById)}: id must be more 0");

			Product productResult = null;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(SpSelectById, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				command.Parameters.Add(idParam);

				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					productResult = new Product


					{
						IdEntity = reader.GetInt32(0),
						Price = reader.GetDecimal(1),
						ProductStatus = new ProductStatus(reader.GetString(2)),
						IdProductInformation = reader.GetInt32(3),
						ProductInformation = _productInformationRepository.SelectById(reader.GetInt32(3))
					};

				}

				connection.Close();

			}

			return productResult;

		}
		public List<Product> SelectAll()
		{
			
			List<Product> products = null;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(SpSelectAll, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};


				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					products = new List<Product>();

					while (reader.Read())
					{
						var product = new Product
						{
							IdEntity = reader.GetInt32(0),
							Price = reader.GetDecimal(1),
							ProductStatus = new ProductStatus(reader.GetString(2)),
							IdProductInformation = reader.GetInt32(3),
							ProductInformation = _productInformationRepository.SelectById(reader.GetInt32(3))
						};

						products.Add(product);

					}
				}


			}

			return products;

			
		}
		public List<Product> Find(Func<Product, bool> predicate)
		{
			
			var allList = SelectAll();

			var result = allList.Where(predicate);

			return result.ToList();
			
		}


		public int? Insert(Product item)
		{
			
			var priceParam = new SqlParameter
			{
				ParameterName = "@Price",
				Value = item.Price
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.ProductStatus.GetStatusName()
			};
			var productInformationIdParam = new SqlParameter
			{
				ParameterName = "@ProductInformationId",
				Value = item.IdProductInformation
			};

			var resultCommand = ExecuteCommand(SpInsert, priceParam, statusParam, productInformationIdParam);

			int? result = null;

			if (resultCommand != null)
			{
				decimal lastId = (decimal)resultCommand;
				result = Decimal.ToInt32(lastId);
			}

			return result;

			
		}
		public bool Update(Product item)
		{
			
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			var priceParam = new SqlParameter
			{
				ParameterName = "@Price",
				Value = item.Price
			};
			var statusParam = new SqlParameter
			{
				ParameterName = "@Status",
				Value = item.ProductStatus.GetStatusName()
			};
			var productInformationIdParam = new SqlParameter
			{
				ParameterName = "@ProductInformationId",
				Value = item.IdProductInformation
			};

			var resultCommand = ExecuteCommand(SpUpdate, idParam, priceParam, statusParam, productInformationIdParam);

			return resultCommand != null && (int)resultCommand == 1;
			
		}
		public bool Delete(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(ProductRepository)}-{nameof(Delete)}: id must be more 0");

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			var resultCommand = ExecuteCommand(SpDelete, idParam);

			return resultCommand != null && (int)resultCommand == 1;
			
		}




		public int GetCountDependencies(int id)
		{
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			object resultCommand;

			resultCommand = ExecuteCommand(SpGetDependencies, idParam);

			if (resultCommand != null)
				return (int)resultCommand;

			return -1;
		}
	}
}
