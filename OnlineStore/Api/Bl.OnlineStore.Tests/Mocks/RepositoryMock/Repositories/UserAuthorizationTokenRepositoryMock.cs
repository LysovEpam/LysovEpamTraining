//using System;
//using System.Collections.Generic;
//using System.Linq;
//using CommonEntities;
//using CommonEntities.Additional;
//using DALContracts.Repositories;

//namespace BL.OnlineStore.Tests.Mocks.RepositoryMock.Repositories
//{
//	public class UserAuthorizationTokenRepositoryMock : IRepositoryUserAuthorizationToken
//	{
//		private readonly List<UserAuthorizationToken> _list;

//		public UserAuthorizationTokenRepositoryMock()
//		{
//			_list = new List<UserAuthorizationToken>();

//			for (int i =1; i < 10; i++)
//			{
//				UserAuthorizationToken userAuthorizationToken = new UserAuthorizationToken(i, DateTime.MinValue, DateTime.MaxValue, "123456",
//					AuthorizationStatus.GetStatusName(AuthorizationStatus.AuthorizationStatusEnum.Active), i);

//				_list.Add(userAuthorizationToken);
//			}
//		}


//		public bool CancelSessionKeys(int idUser, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
//		{
//			return true;
//		}

//		public bool CancelSessionKeys(UserSystem userSystem, AuthorizationStatus oldStatus, DateTime dateBlock, AuthorizationStatus newStatus)
//		{
//			return true;
//		}

//		public int? Insert(UserAuthorizationToken item)
//		{
//			int idEntity = _list.Last().IdEntity.Value;

//			item.IdEntity = idEntity;

//			_list.Add(item);

//			return idEntity + 1;
//		}

//		public bool Delete(int id)
//		{
//			UserAuthorizationToken user = SelectById(id);

//			bool result =_list.Remove(user);

//			return result;
//		}

//		public List<UserAuthorizationToken> Find(Func<UserAuthorizationToken, bool> predicate)
//		{
//			return _list.Where(predicate).ToList();
//		}
//		public List<UserAuthorizationToken> SelectAll()
//		{
//			return _list;
//		}

//		public UserAuthorizationToken SelectById(int id)
//		{
//			foreach (UserAuthorizationToken userAuthorization in _list)
//			{
//				if (userAuthorization.IdEntity.Value == id)
//					return userAuthorization;
//			}

//			return null;
//		}

//		public UserAuthorizationToken GetByToken(string token)
//		{
//			throw new NotImplementedException();
//		}

//		public bool Update(UserAuthorizationToken item)
//		{
//			foreach (UserAuthorizationToken userAuthorization in _list)
//			{
//				if (userAuthorization.IdEntity.Value == item.IdEntity.Value)
//				{
//					userAuthorization.AuthorizationStatus = item.AuthorizationStatus;
//					userAuthorization.FinishSession = item.FinishSession;
//					userAuthorization.UserToken = item.UserToken;
//					userAuthorization.StartSession = item.StartSession;
//					userAuthorization.AuthorizationStatus = item.AuthorizationStatus;
//					userAuthorization.UserSystem = item.UserSystem;
//					userAuthorization.UserId = item.UserId;

//					return true;

//				}

				
//			}

//			return false;
//		}

//		public int GetCountDependencies(int id)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
