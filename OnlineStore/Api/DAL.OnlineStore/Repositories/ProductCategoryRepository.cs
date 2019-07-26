using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductCategoryRepository : ExecuteCommandBase, IRepository<ProductCategory>
	{

		#region Stored procedure names

		const string SpSelectById = @"ProductCategory_SelectById";
		const string SpSelectAll = @"ProductCategory_SelectAll";
		const string SpInsert = @"ProductCategory_Insert";
		const string SpUpdate = @"ProductCategory_Update";
		const string SpDelete = @"ProductCategory_Delete";
		const string SpGetDependencies = @"ProductCategory_CountDependencies";

		#endregion

		private readonly string _connectionString;

		public ProductCategoryRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}

		

		public ProductCategory SelectById(int id)
		{

			if(id<1)
				throw new Exception($"Exception in {nameof(ProductCategoryRepository)}-{nameof(SelectById)}: id must be more 0");

			ProductCategory categoryResult = null;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var command = new SqlCommand(SpSelectById, connection)
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

					categoryResult = new ProductCategory
					{
						IdEntity = reader.GetInt32(0),
						CategoryName = reader.GetString(1),
						Description = reader.GetString(2)
					};


				}

				connection.Close();

			}

			return categoryResult;

		}
		public List<ProductCategory> SelectAll()
		{

			List<ProductCategory> productCategories = null;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var command = new SqlCommand(SpSelectAll, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};


				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					productCategories = new List<ProductCategory>();

					while (reader.Read())
					{

						var productCategory = new ProductCategory
						{
							IdEntity = reader.GetInt32(0),
							CategoryName = reader.GetString(1),
							Description = reader.GetString(2)
						};

						productCategories.Add(productCategory);

					}
				}


			}

			return productCategories;


		}
		public List<ProductCategory> Find(Func<ProductCategory, bool> predicate)
		{
			var allList = SelectAll();

			var result = allList.Where(predicate);

			return result.ToList();
		}


		public int? Insert(ProductCategory item)
		{
			var categoryParam = new SqlParameter
			{
				ParameterName = "@CategoryName",
				Value = item.CategoryName
			};
			var descriptionParam = new SqlParameter
			{
				ParameterName = "@Description",
				Value = item.Description
			};

			var resultCommand = ExecuteCommand(SpInsert, categoryParam, descriptionParam);

			int? result = null;

			if (resultCommand != null)
			{
				decimal lastId = (decimal)resultCommand;
				result = Decimal.ToInt32(lastId);
			}

			return result;


		}
		public bool Update(ProductCategory item)
		{
			if (!item.IdEntity.HasValue)
				return false;

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			var categoryParam = new SqlParameter
			{
				ParameterName = "@CategoryName",
				Value = item.CategoryName
			};
			var descriptionParam = new SqlParameter
			{
				ParameterName = "@Description",
				Value = item.Description
			};

			var resultCommand = ExecuteCommand(SpUpdate, idParam, categoryParam, descriptionParam);

			return resultCommand != null && (int)resultCommand == 1;

		}
		public bool Delete(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(ProductCategoryRepository)}-{nameof(Delete)}: id must be more 0");

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			object resultCommand;

			resultCommand = ExecuteCommand(SpDelete, idParam);

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
				return (int) resultCommand;

			return -1;

		}
	}
}
