using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductRepository : ExecuteCommandBase, IRepository<Product>, ILogRepository
	{
		#region Stored procedure names

		const string SpSelectById = @"Product_SelectById";
		const string SpSelectAll = @"Product_SelectAll";
		const string SpInsert = @"Product_Insert";
		const string SpUpdate = @"Product_Update";
		const string SpDelete = @"Product_Delete";

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
			try
			{
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

						int readId = reader.GetInt32(0);
						decimal readPrice = reader.GetDecimal(1);
						string readStatus = reader.GetString(2);
						int readIdProductInformation = reader.GetInt32(3);

						ProductInformation productInformation =
							_productInformationRepository.SelectById(readIdProductInformation);

						productResult = new Product(readId, readPrice, readStatus, productInformation);

					}

					connection.Close();

				}

				return productResult;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(SelectById)}",
					$"Ошибка при получении сущности {nameof(Product)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<Product> SelectAll()
		{
			try
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
							int readId = reader.GetInt32(0);
							decimal readPrice = reader.GetDecimal(1);
							string readStatus = reader.GetString(2);
							int readIdProductInformation = reader.GetInt32(3);

							ProductInformation productInformation =
								_productInformationRepository.SelectById(readIdProductInformation);

							var product = new Product(readId, readPrice, readStatus, productInformation);

							products.Add(product);

						}
					}


				}

				return products;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(SelectAll)}",
					$"Ошибка при получении списка сущностей {nameof(Product)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<Product> Find(Func<Product, bool> predicate)
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
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Find)}",
					$"Ошибка при поиске в списке сущностей {nameof(Product)}",
					$"Текст исключения: {e.Message}");

				return null;

			}
		}


		public int? Insert(Product item)
		{
			try
			{
				var priceParam = new SqlParameter
				{
					ParameterName = "@Price",
					Value = item.Price
				};
				var statusParam = new SqlParameter
				{
					ParameterName = "@Status",
					Value = item.Status
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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Insert)}",
					$"Ошибка при создании новой сущности {nameof(Product)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public bool Update(Product item)
		{
			if (!item.IdEntity.HasValue)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(Product)}",
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
				var priceParam = new SqlParameter
				{
					ParameterName = "@Price",
					Value = item.Price
				};
				var statusParam = new SqlParameter
				{
					ParameterName = "@Status",
					Value = item.Status
				};
				var productInformationIdParam = new SqlParameter
				{
					ParameterName = "@ProductInformationId",
					Value = item.IdProductInformation
				};

				var resultCommand = ExecuteCommand(SpUpdate, idParam, priceParam, statusParam, productInformationIdParam);

				return resultCommand != null && (int)resultCommand == 1;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(Product)}",
					$"Текст исключения: {e.Message}");

				return false;
			}
		}
		public bool Delete(int id)
		{
			if (id < 1)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(Product)}",
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

				var resultCommand = ExecuteCommand(SpDelete, idParam);

				return resultCommand != null && (int)resultCommand == 1;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(ProductRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(Product)}",
					$"Текст инсключения: {e.Message}");

				return false;
			}
		}


		public event RepositoryEvent RepositoryEvent;


		private void DoRepositoryEvent(string location, string caption, string description)
		{
			RepositoryEvent?.Invoke(location, caption, description);
		}




	}
}
