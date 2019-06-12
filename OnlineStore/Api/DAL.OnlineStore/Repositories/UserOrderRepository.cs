using System;
using System.Collections.Generic;
using CommonEntities;
using DALContracts;
using DALContracts.Repositories;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.OnlineStore.Repositories
{
	public class UserOrderRepository : ExecuteCommandBase, IRepository<UserOrder>, ILogRepository
	{

		#region Stored procedure names

		const string SpSelectById = @"UserOrder_SelectById";
		const string SpSelectAll = @"UserOrder_SelectAll";
		const string SpInsert = @"UserOrder_Insert";
		const string SpUpdate = @"UserOrder_Update";
		const string SpDelete = @"UserOrder_Delete";

		private const string SpInsertUserOrderProduct = @"UserOrderProduct_Insert";
		private const string SpDeleteUserOrderProduct = @"UserOrderProduct_Delete";

		#endregion

		private readonly string _connectionString;
		private readonly ProductRepository _productRepository;
		private readonly UserSystemRepository _userSystemRepository;

		public UserOrderRepository(string connectionString, ProductRepository productRepository, UserSystemRepository userSystemRepository) : base(connectionString)
		{
			_connectionString = connectionString;
			_productRepository = productRepository;
			_userSystemRepository = userSystemRepository;
		}

		public event RepositoryEvent RepositoryEvent;



		public int? Insert(UserOrder item)
		{
			try
			{

				int? idNewOrder;

				var dateOrderParam = new SqlParameter
				{
					ParameterName = "@DateOrder",
					Value = item.DateOrder
				};
				var addressParam = new SqlParameter
				{
					ParameterName = "@Address",
					Value = item.Address
				};
				var statusParam = new SqlParameter
				{
					ParameterName = "@Status",
					Value = item.Status
				};
				var userIdParam = new SqlParameter
				{
					ParameterName = "@UserId",
					Value = item.UserId
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


					command.Parameters.Add(dateOrderParam);
					command.Parameters.Add(addressParam);
					command.Parameters.Add(statusParam);
					command.Parameters.Add(userIdParam);

					var idCommandResult = command.ExecuteScalar();

					idNewOrder = Decimal.ToInt32((decimal)idCommandResult);

					if (item.Products != null)
					{
						foreach (var product in item.Products)
						{
							command = new SqlCommand(SpInsertUserOrderProduct, connection)
							{
								CommandType = System.Data.CommandType.StoredProcedure,
								Transaction = transaction
							};

							var idProductInformationParam = new SqlParameter
							{
								ParameterName = "@UserOrderId",
								Value = idNewOrder
							};
							var idCategoryParam = new SqlParameter
							{
								ParameterName = "@ProductId",
								// ReSharper disable once PossibleInvalidOperationException
								Value = product.IdEntity.Value
							};

							command.Parameters.Add(idProductInformationParam);
							command.Parameters.Add(idCategoryParam);

							command.ExecuteNonQuery();
						}
					}


					transaction.Commit();

					connection.Close();
				}

				return idNewOrder;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(Insert)}",
					$"Ошибка при создании новой сущности {nameof(UserOrder)}",
					$"Текст исключения: {e.Message}");

				return null;
			}


		}
		public bool Update(UserOrder item)
		{
			if (!item.IdEntity.HasValue)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(UserOrder)}",
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
				var dateOrderParam = new SqlParameter
				{
					ParameterName = "@DateOrder",
					Value = item.DateOrder
				};
				var addressParam = new SqlParameter
				{
					ParameterName = "@Address",
					Value = item.Address
				};
				var statusParam = new SqlParameter
				{
					ParameterName = "@Status",
					Value = item.Status
				};
				var userIdParam = new SqlParameter
				{
					ParameterName = "@UserId",
					Value = item.UserId
				};

				object idCommandResult;

				//Старая запись, сохраненная в БД
				UserOrder oldInformation = SelectById(item.IdEntity.Value);

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
					command.Parameters.Add(dateOrderParam);
					command.Parameters.Add(addressParam);
					command.Parameters.Add(statusParam);
					command.Parameters.Add(userIdParam);

					idCommandResult = command.ExecuteScalar();

					#region Если список продуктов не пустой, то обновить инфоформацию о продуктах в заказе

					if (item.Products != null)
					{

						#region Удаление списка продуктов

						foreach (var product in oldInformation.Products)
						{
							var commandRemove = new SqlCommand(SpDeleteUserOrderProduct, connection)
							{
								CommandType = System.Data.CommandType.StoredProcedure,
								Transaction = transaction
							};

							var idOrderParam = new SqlParameter
							{
								ParameterName = "@UserOrderId",
								// ReSharper disable once PossibleInvalidOperationException
								Value = oldInformation.IdEntity.Value
							};
							var idProductParam = new SqlParameter
							{
								ParameterName = "@ProductId",
								// ReSharper disable once PossibleInvalidOperationException
								Value = product.IdEntity.Value
							};

							commandRemove.Parameters.Add(idOrderParam);
							commandRemove.Parameters.Add(idProductParam);

							commandRemove.ExecuteNonQuery();
						}

						#endregion

						#region Добавление продуктов, которых нет в старом списке

						foreach (var product in item.Products)
						{
							var commandAdd = new SqlCommand(SpInsertUserOrderProduct, connection)
							{
								CommandType = System.Data.CommandType.StoredProcedure,
								Transaction = transaction
							};

							var idOrderParam = new SqlParameter
							{
								ParameterName = "@UserOrderId",
								Value = item.IdEntity.Value
							};
							var idProductParam = new SqlParameter
							{
								ParameterName = "@ProductId",
								// ReSharper disable once PossibleInvalidOperationException
								Value = product.IdEntity.Value
							};

							commandAdd.Parameters.Add(idOrderParam);
							commandAdd.Parameters.Add(idProductParam);

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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(Update)}",
					$"Ошибка при обновлении сущности {nameof(UserOrder)}",
					$"Текст исключения: {e.Message}");

				return false;
			}





		}

		public bool Delete(int id)
		{
			try
			{
				UserOrder oldInformation = SelectById(id);
				object idCommandResult;

				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();

					SqlTransaction transaction = connection.BeginTransaction();

					#region Удаление списка продуктов

					foreach (var product in oldInformation.Products)
					{
						var commandRemove = new SqlCommand(SpDeleteUserOrderProduct, connection)
						{
							CommandType = System.Data.CommandType.StoredProcedure,
							Transaction = transaction
						};

						var idOrderParam = new SqlParameter
						{
							ParameterName = "@UserOrderId",
							// ReSharper disable once PossibleInvalidOperationException
							Value = oldInformation.IdEntity.Value
						};
						var idProductParam = new SqlParameter
						{
							ParameterName = "@ProductId",
							// ReSharper disable once PossibleInvalidOperationException
							Value = product.IdEntity.Value
						};

						commandRemove.Parameters.Add(idOrderParam);
						commandRemove.Parameters.Add(idProductParam);

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
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(Delete)}",
					$"Ошибка при удалении сущности {nameof(UserOrder)}",
					$"Текст инсключения: {e.Message}");

				return false;
			}
		}


		public UserOrder SelectById(int id)
		{
			try
			{
				UserOrder userOrder = null;

				var idParam = new SqlParameter
				{
					ParameterName = "@IdEntity",
					Value = id
				};

				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();

					SqlCommand commandSelectUserOrder = new SqlCommand(SpSelectById, connection)
					{
						CommandType = System.Data.CommandType.StoredProcedure
					};

					commandSelectUserOrder.Parameters.Add(idParam);

					SqlDataReader reader = commandSelectUserOrder.ExecuteReader();

					if (reader.HasRows)
					{
						reader.Read();

						int readId = reader.GetInt32(0);
						DateTime readDate = reader.GetDateTime(1);
						string readAddress = reader.GetString(2);
						string readStatus = reader.GetString(3);
						int readIdUser = reader.GetInt32(4);
						int readIdProduct = reader.GetInt32(5);

						Product product = _productRepository.SelectById(readIdProduct);
						UserSystem userSystem = _userSystemRepository.SelectById(readIdUser);

						userOrder = new UserOrder(readId, readDate, readAddress, readStatus, userSystem, new List<Product>());
						userOrder.Products.Add(product);

						while (reader.Read())
						{
							readIdProduct = reader.GetInt32(5);

							product = _productRepository.SelectById(readIdProduct);

							userOrder.Products.Add(product);
						}
					}
				}

				return userOrder;

			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(SelectById)}",
					$"Ошибка при получении сущности {nameof(UserOrder)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<UserOrder> SelectAll()
		{
			try
			{
				List<UserOrder> listResult = null;

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
						listResult = new List<UserOrder>();

						reader.Read();

						int readId = reader.GetInt32(0);
						DateTime readDate = reader.GetDateTime(1);
						string readAddress = reader.GetString(2);
						string readStatus = reader.GetString(3);
						int readIdUser = reader.GetInt32(4);
						int readIdProduct = reader.GetInt32(5);

						Product product = _productRepository.SelectById(readIdProduct);
						UserSystem userSystem = _userSystemRepository.SelectById(readIdUser);

						UserOrder order = new UserOrder(readId, readDate, readAddress, readStatus, userSystem, new List<Product>());
						order.Products.Add(product);
						listResult.Add(order);


						while (reader.Read())
						{
							readId = reader.GetInt32(0);
							readIdProduct = reader.GetInt32(5);

							// ReSharper disable once PossibleInvalidOperationException
							if (listResult.Last().IdEntity.Value == readId)
							{
								product = _productRepository.SelectById(readIdProduct);
								listResult.Last().Products.Add(product);
							}
							else
							{
								readDate = reader.GetDateTime(1);
								readAddress = reader.GetString(2);
								readStatus = reader.GetString(3);
								readIdUser = reader.GetInt32(4);
								userSystem = _userSystemRepository.SelectById(readIdUser);

								order = new UserOrder(readId, readDate, readAddress, readStatus, userSystem, new List<Product>());
								product = _productRepository.SelectById(readIdProduct);
								order.Products.Add(product);
								listResult.Add(order);
							}

						}
					}
				}

				return listResult;
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(SelectAll)}",
					$"Ошибка при получении списка сущностей {nameof(UserOrder)}",
					$"Текст исключения: {e.Message}");

				return null;
			}
		}
		public List<UserOrder> Find(Func<UserOrder, bool> predicate)
		{
			try
			{
				var tempList = SelectAll();

				var resultList = tempList.Where(predicate);

				return resultList.ToList();
			}
			catch (Exception e)
			{
				DoRepositoryEvent(
					$"DAL.OnlineStore - {nameof(UserOrderRepository)} - {nameof(Find)}",
					$"Ошибка при поиске в списке сущностей {nameof(UserOrder)}",
					$"Текст исключения: {e.Message}");

				return null;
			}

		}




		private void DoRepositoryEvent(string location, string caption, string description)
		{
			RepositoryEvent?.Invoke(location, caption, description);
		}


	}
}
