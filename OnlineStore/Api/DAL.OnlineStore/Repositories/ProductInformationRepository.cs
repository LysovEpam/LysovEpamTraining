using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CommonEntities;
using DALContracts.Repositories;

namespace DAL.OnlineStore.Repositories
{
	public class ProductInformationRepository : ExecuteCommandBase, IRepository<ProductInformation>
	{
		#region Stored procedure names

		const string SpSelectById = @"ProductInformation_SelectById";
		const string SpSelectAll = @"ProductInformation_SelectAll";
		const string SpInsert = @"ProductInformation_Insert";
		const string SpUpdate = @"ProductInformation_Update";
		const string SpDelete = @"ProductInformation_Delete";
		const string SpCountDependencies = @"ProductInformation_CountDependencies";

		private const string SpInsertProductInformationProductCategory = @"ProductInformationProductCategory_Insert";
		private const string SpDeleteProductInformationProductCategory = @"ProductInformationProductCategory_Delete";




		#endregion

		private readonly string _connectionString;
		private readonly ProductCategoryRepository _productCategoryRepository;

		public ProductInformationRepository(string connectionString, ProductCategoryRepository productCategoryRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_productCategoryRepository = productCategoryRepository;
		}



		public ProductInformation SelectById(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(ProductInformationRepository)}-{nameof(SelectById)}: id must be more 0");

			ProductInformation productInformation = null;

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand commandSelectProductInformation = new SqlCommand(SpSelectById, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};

				commandSelectProductInformation.Parameters.Add(idParam);

				SqlDataReader reader = commandSelectProductInformation.ExecuteReader();

				if (reader.HasRows)
				{
					reader.Read();

					int readId = reader.GetInt32(0);
					string readName = reader.GetString(1);
					string readImage = reader.GetString(2);
					string readDescription = reader.GetString(3);
					int readIdCategory = reader.GetInt32(4);

					ProductCategory productCategory = _productCategoryRepository.SelectById(readIdCategory);

					productInformation = new ProductInformation(readId, readName, readImage, readDescription, new List<ProductCategory>());
					productInformation.ProductCategories.Add(productCategory);

					while (reader.Read())
					{
						readIdCategory = reader.GetInt32(4);

						productCategory = _productCategoryRepository.SelectById(readIdCategory);

						productInformation.ProductCategories.Add(productCategory);
					}
				}
			}

			return productInformation;



		}

		public List<ProductInformation> SelectAll()
		{
			
			List<ProductInformation> listResult = null;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlCommand commandSelectProductInformation = new SqlCommand(SpSelectAll, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure
				};

				SqlDataReader reader = commandSelectProductInformation.ExecuteReader();

				if (reader.HasRows)
				{
					listResult = new List<ProductInformation>();

					reader.Read();

					int readId = reader.GetInt32(0);
					string readName = reader.GetString(1);
					string readImage = reader.GetString(2);
					string readDescription = reader.GetString(3);
					int readIdCategory = reader.GetInt32(4);

					ProductCategory productCategory = _productCategoryRepository.SelectById(readIdCategory);

					ProductInformation productInformation = new ProductInformation(readId, readName, readImage, readDescription, new List<ProductCategory>());
					productInformation.ProductCategories.Add(productCategory);
					listResult.Add(productInformation);

					while (reader.Read())
					{
						readId = reader.GetInt32(0);
						readIdCategory = reader.GetInt32(4);

						// ReSharper disable once PossibleInvalidOperationException
						if (listResult.Last().IdEntity.Value == readId)
						{
							productCategory = _productCategoryRepository.SelectById(readIdCategory);
							listResult.Last().ProductCategories.Add(productCategory);
						}
						else
						{
							readName = reader.GetString(1);
							readImage = reader.GetString(2);
							readDescription = reader.GetString(3);

							productInformation = new ProductInformation(readId, readName, readImage, readDescription, new List<ProductCategory>());
							productCategory = _productCategoryRepository.SelectById(readIdCategory);
							productInformation.ProductCategories.Add(productCategory);
							listResult.Add(productInformation);
						}

					}
				}
			}

			return listResult;

		}

		public List<ProductInformation> Find(Func<ProductInformation, bool> predicate)
		{
			
			var tempList = SelectAll();

			var resultList = tempList.Where(predicate);

			return resultList.ToList();
			
		}

		public int? Insert(ProductInformation item)
		{
			
			int? idNewProductInformation;

			var productNameParam = new SqlParameter
			{
				ParameterName = "@ProductName",
				Value = item.ProductName
			};
			var imageParam = new SqlParameter
			{
				ParameterName = "@ImageLocalSource",
				Value = item.ImageLocalSource
			};
			var descriptionParam = new SqlParameter
			{
				ParameterName = "@Description",
				Value = item.Description
			};

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();

				SqlCommand command = new SqlCommand(SpInsert, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					Transaction = transaction
				};

				command.Parameters.Add(productNameParam);
				command.Parameters.Add(imageParam);
				command.Parameters.Add(descriptionParam);

				var idCommandResult = command.ExecuteScalar();

				idNewProductInformation = Decimal.ToInt32((decimal)idCommandResult);

				if (item.ProductCategories != null)
				{
					foreach (var listCategory in item.ProductCategories)
					{
						command = new SqlCommand(SpInsertProductInformationProductCategory, connection)
						{
							CommandType = System.Data.CommandType.StoredProcedure,
							Transaction = transaction
						};

						var idProductInformationParam = new SqlParameter
						{
							ParameterName = "@ProductInformationId",
							Value = idNewProductInformation
						};
						var idCategoryParam = new SqlParameter
						{
							ParameterName = "@ProductCategoryId",
							// ReSharper disable once PossibleInvalidOperationException
							Value = listCategory.IdEntity.Value
						};

						command.Parameters.Add(idProductInformationParam);
						command.Parameters.Add(idCategoryParam);

						command.ExecuteNonQuery();
					}
				}

				transaction.Commit();

				connection.Close();

			}

			return idNewProductInformation;
			
		}

		public bool Update(ProductInformation item)
		{

			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = item.IdEntity.Value
			};
			var productNameParam = new SqlParameter
			{
				ParameterName = "@ProductName",
				Value = item.ProductName
			};
			var imageParam = new SqlParameter
			{
				ParameterName = "@ImageLocalSource",
				Value = item.ImageLocalSource
			};
			var descriptionParam = new SqlParameter
			{
				ParameterName = "@Description",
				Value = item.Description
			};


			object idCommandResult;

			//Старая запись, сохраненная в БД
			ProductInformation oldInformation = SelectById(item.IdEntity.Value);

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();

				SqlCommand command = new SqlCommand(SpUpdate, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					Transaction = transaction
				};

				command.Parameters.Add(idParam);
				command.Parameters.Add(productNameParam);
				command.Parameters.Add(imageParam);
				command.Parameters.Add(descriptionParam);

				idCommandResult = command.ExecuteScalar();

				#region Если список категорий не пустой, то обновить информацию о категориях товара

				if (item.ProductCategories != null)
				{

					#region Удаление списка категорий

					foreach (var productCategory in oldInformation.ProductCategories)
					{
						var commandRemove = new SqlCommand(SpDeleteProductInformationProductCategory, connection)
						{
							CommandType = System.Data.CommandType.StoredProcedure,
							Transaction = transaction
						};

						var idProductParam = new SqlParameter
						{
							ParameterName = "@IdProductInformation",
							// ReSharper disable once PossibleInvalidOperationException
							Value = oldInformation.IdEntity.Value
						};
						var idCategoryParam = new SqlParameter
						{
							ParameterName = "@IdCategory",
							// ReSharper disable once PossibleInvalidOperationException
							Value = productCategory.IdEntity.Value
						};

						commandRemove.Parameters.Add(idProductParam);
						commandRemove.Parameters.Add(idCategoryParam);

						commandRemove.ExecuteNonQuery();
					}

					#endregion

					#region Добавление категорий, которых нет в старом списке

					foreach (var productCategory in item.ProductCategories)
					{
						var commandAdd = new SqlCommand(SpInsertProductInformationProductCategory, connection)
						{
							CommandType = System.Data.CommandType.StoredProcedure,
							Transaction = transaction
						};

						var idProductParam = new SqlParameter
						{
							ParameterName = "@ProductInformationId",
							Value = item.IdEntity.Value
						};
						var idCategoryParam = new SqlParameter
						{
							ParameterName = "@ProductCategoryId",
							// ReSharper disable once PossibleInvalidOperationException
							Value = productCategory.IdEntity.Value
						};

						commandAdd.Parameters.Add(idProductParam);
						commandAdd.Parameters.Add(idCategoryParam);

						commandAdd.ExecuteNonQuery();
					}

					#endregion

				}

				#endregion

				transaction.Commit();

				connection.Close();

			}

			return idCommandResult != null && (int)idCommandResult == 1;

			
		}

		public bool Delete(int id)
		{
			if (id < 1)
				throw new Exception($"Exception in {nameof(ProductInformationRepository)}-{nameof(Delete)}: id must be more 0");

			ProductInformation oldInformation = SelectById(id);
			object idCommandResult;

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				SqlTransaction transaction = connection.BeginTransaction();

				#region Delete list categories

				foreach (var productCategory in oldInformation.ProductCategories)
				{
					var commandRemove = new SqlCommand(SpDeleteProductInformationProductCategory, connection)
					{
						CommandType = System.Data.CommandType.StoredProcedure,
						Transaction = transaction
					};

					var idProductParam = new SqlParameter
					{
						ParameterName = "@IdProductInformation",
						// ReSharper disable once PossibleInvalidOperationException
						Value = oldInformation.IdEntity.Value
					};
					var idCategoryParam = new SqlParameter
					{
						ParameterName = "@IdCategory",
						// ReSharper disable once PossibleInvalidOperationException
						Value = productCategory.IdEntity.Value
					};

					commandRemove.Parameters.Add(idProductParam);
					commandRemove.Parameters.Add(idCategoryParam);

					commandRemove.ExecuteNonQuery();

				}

				#endregion

				var command = new SqlCommand(SpDelete, connection)
				{
					CommandType = System.Data.CommandType.StoredProcedure,
					Transaction = transaction
				};

				var idEntityParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				command.Parameters.Add(idEntityParam);

				idCommandResult = command.ExecuteScalar();


				transaction.Commit();

				connection.Close();


			}

			return idCommandResult != null && (int)idCommandResult == 1;

		

		}

		public int GetCountDependencies(int id)
		{
			var idParam = new SqlParameter
			{
				ParameterName = "@IdEntity",
				Value = id
			};

			object resultCommand;

			resultCommand = ExecuteCommand(SpCountDependencies, idParam);

			if (resultCommand != null)
				return (int)resultCommand;

			return -1;
		}
	}
}
