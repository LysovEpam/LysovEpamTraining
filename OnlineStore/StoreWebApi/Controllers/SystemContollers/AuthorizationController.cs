using BLContracts.SystemBl;
using CommonEntities.Additional;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreWebApi.Model.ApiJsonResult;


namespace StoreWebApi.Controllers.SystemContollers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly IAuthorizationBlModel _registrationBlModel;
		public AuthorizationController(IAuthorizationBlModel registrationBlModel)
		{
			_registrationBlModel = registrationBlModel;
		}

		[HttpPost]
		public string Authorization(string login, string password)
		{
			BaseJsonResult jsonResult = new BaseJsonResult(false, false, null, null);

			UserStatus userStatus = _registrationBlModel.CheckUserStatus(login, password);

			if(userStatus.Status == UserStatus.StatusEnum.Block)
				jsonResult = new BaseJsonResult(true, false, "Пользователь заблокирован в системе", userStatus.GetStatusName());
			if (userStatus.Status == UserStatus.StatusEnum.Delete)
				jsonResult = new BaseJsonResult(true, false, "Пользователь удален", userStatus.GetStatusName());
			if (userStatus.Status == UserStatus.StatusEnum.Active)
			{
				(UserStatus, string) token = _registrationBlModel.GetAuthorizationToken(login, password);

				if(token.Item1.Status == UserStatus.StatusEnum.Active && token.Item2!=null)
					jsonResult = new BaseJsonResult(true, true, "", token.Item2);
				else
					jsonResult = new BaseJsonResult(true, false, "Ошибка авторизации", null);
				

			}
			
			string serializedData = JsonConvert.SerializeObject(jsonResult);
			return serializedData;
		}

	}
}
