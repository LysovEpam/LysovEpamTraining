using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductCategoryRepository : ExecuteCommandBase, IRepository<ProductCategory>, ILogRepository
	{

		#region Stored procedure names

		const string SpSelectById = @"ProductCategory_SelectById";
		const string SpSelectAll = @"ProductCategory_SelectAll";
		const string SpInsert = @"ProductCategory_Insert";
		const string SpUpdate = @"ProductCategory_Update";
		const string SpDelete = @"ProductCategory_Delete";

		#endregion

		private readonly string _connectionString;

		public ProductCategoryRepository(string connectionString) : base(connectionString)
		{
			_connectionString = connectionString;
		}
		
		public event RepositoryEvent RepositoryEvent;
		

		public ProductCategory SelectById(int id)
		{
			try
			{
				ProductCategory categoryResult = null;

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

						int readId = reader.GetInt32(0);
						string readCategory = reader.GetString(1);
						string readDescription = reader.GetString(2);

						categoryResult = new ProductCategory(readId, readCategory, readDescription);
						
					}

					connection.Close();

				}

				return categoryResult;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(SelectById)}",
					$"Ошибка при получении сущности {nameof(ProductCategory)}",
					$"Текст исключения: {e.Message}");

				return null;
			}



		}
		public List<ProductCategory> SelectAll()
		{
			try
			{
				List<ProductCategory> productCategories = null;

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
						productCategories = new List<ProductCategory>();

						while (reader.Read())
						{
							int readId = reader.GetInt32(0);
							string readCategory = reader.GetString(1);
							string readDescription = reader.GetString(2);

							ProductCategory productCategory = new ProductCategory(readId, readCategory, readDescription);

							productCategories.Add(productCategory);

						}
					}


				}

				return productCategories;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(SelectAll)}",
					$"Ошибка при получении списка сущностей {nameof(ProductCategory)}",
					$"Текст исключения: {e.Message}");

				return null;
			}


		}
		public List<ProductCategory> Find(Func<ProductCategory, bool> predicate)
		{
			try
			{
				var allList = SelectAll();

				var result = allList.Where(predicate);

				return result.ToList();
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Find)}",
					$"Ошибка при поиске в списке сущностей {nameof(ProductCategory)}",
					$"Текст исключения: {e.Message}");

				return null;

			}

		}


		public int? Insert(ProductCategory item)
		{
			try
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Insert)}",
					$"Ошибка при создании новой сущности {nameof(ProductCategory)}",
					$"Текст исключения: {e.Message}");

				return null;
			}

		}
		public bool Update(ProductCategory item)
		{
			if (!item.IdEntity.HasValue)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(ProductCategory)}",
					"Id сущности не может быть пустым");

				return false;
			}
			
			try
			{
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(ProductCategory)}",
					$"Текст исключения: {e.Message}");

				return false;
			}

			
		}
		public bool Delete(int id)
		{
			if (id < 1)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(ProductCategory)}",
					"Id не может быть меньше 1");

				return false;
			}

			try
			{
				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				object resultCommand;

				resultCommand = ExecuteCommand(SpDelete, idParam);

				return resultCommand != null && (int)resultCommand == 1;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductCategoryRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(ProductCategory)}",
					$"Текст инсключения: {e.Message}");

				return false;
			}


		}



		private void DoRepositoryEvent(string location, string caption, string description)
		{
			RepositoryEvent?.Invoke(location, caption, description);
		}



	}
}
