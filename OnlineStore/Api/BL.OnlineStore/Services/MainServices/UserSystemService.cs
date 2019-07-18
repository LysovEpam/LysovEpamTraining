using System;
using System.Collections.Generic;
using System.Linq;
using BL.OnlineStore.BlModels;
using BLContracts;
using BLContracts.ActionResults;
using BLContracts.MainService;
using CommonEntities;
using DALContracts;

namespace BL.OnlineStore.Services.MainServices
{
	public class UserSystemService : BaseBlModel, IUserSystemService
	{
		private readonly IDbContext _dbContext;

		public UserSystemService(IDbContext dbContext, IProgramLogRegister programLogRegister)
			: base(programLogRegister)
		{
			_dbContext = dbContext;
			
		}


		public (ServiceResult, UserSystem) GetById(int id)
		{
			var userSystem = _dbContext.UsersSystem.SelectById(id);

			ServiceResult actionResult;

			if (userSystem == null)
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
			else
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");
				userSystem.UserAdmittance = null;
			}



			return (actionResult, userSystem);
		}

		public (ServiceResult, UserSystem) GetByLogin(string login)
		{
			if (login == null)
			{
				ServiceResult actionResultError =
					new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "Логин не может быть пустым");

				return (actionResultError, null);
			}

			var list = _dbContext.UsersSystem.Find(c => c.UserAdmittance.Login == login);

			ServiceResult actionResult;

			if (list == null || list.Count != 1)
			{
				actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.SystemError, "");
				return (actionResult, null);
			}

			actionResult = new ServiceResult(ServiceResult.ResultConnectionEnum.Correct, "");

			var user = list.FirstOrDefault();
			user.UserAdmittance = null;

			return (actionResult, user);



		}
		public (ServiceResult, List<UserSystem>) GetAll()
		{
			throw new NotImplementedException();
		}

		public (ServiceResult, UserSystem) GetBySelf(string sessionToken)
		{
			throw new NotImplementedException();
		}


		public ServiceResult UpdateUser(string sessionToken, UserSystem user)
		{
			throw new NotImplementedException();
		}
		public ServiceResult DeleteUser(string sessionToken, int id)
		{
			throw new NotImplementedException();
		}

		
	}
}
