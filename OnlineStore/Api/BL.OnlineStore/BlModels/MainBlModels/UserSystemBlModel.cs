using System;
using System.Collections.Generic;
using System.Linq;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainBl;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.BlModels.MainBlModels
{
	public class UserSystemBlModel : BaseBlModel, IUserSystemBlModel
	{
		private readonly IDbContext _dbContext;

		public UserSystemBlModel(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			_dbContext.RepositoryEvent += DbRepositoryEvent;
		}


		public (BaseActionResult, UserSystem) GetById(int id)
		{
			var userSystem = _dbContext.UsersSystem.SelectById(id);

			BaseActionResult actionResult;

			if (userSystem == null)
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");
				userSystem.UserAccess = null;
			}



			return (actionResult, userSystem);
		}

		public (BaseActionResult, UserSystem) GetByLogin(string login)
		{
			if (login == null)
			{
				BaseActionResult actionResultError =
					new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "Логин не может быть пустым");

				return (actionResultError, null);
			}

			var list = _dbContext.UsersSystem.Find(c => c.UserAccess.Login == login);

			BaseActionResult actionResult;

			if (list == null || list.Count != 1)
			{
				actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.SystemError, "");
				return (actionResult, null);
			}

			actionResult = new BaseActionResult(BaseActionResult.ResultConnectionEnum.Correct, "");

			var user = list.FirstOrDefault();
			user.UserAccess = null;

			return (actionResult, user);



		}
		public (BaseActionResult, List<UserSystem>) GetAll()
		{
			throw new NotImplementedException();
		}

		public (BaseActionResult, UserSystem) GetBySelf(string sessionToken)
		{
			throw new NotImplementedException();
		}


		public BaseActionResult UpdateUser(string sessionToken, UserSystem user)
		{
			throw new NotImplementedException();
		}
		public BaseActionResult DeleteUser(string sessionToken, int id)
		{
			throw new NotImplementedException();
		}

		private void DbRepositoryEvent(string location, string caption, string description)
		{
			SaveEvent(TypeEvent.DbError, location, caption, description);
		}
	}
}
