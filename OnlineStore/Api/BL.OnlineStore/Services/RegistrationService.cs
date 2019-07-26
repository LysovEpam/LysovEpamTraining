using BLContracts.ActionResults;
using BLContracts.Models;
using BLContracts.Services;
using CommonEntities.Additional;

namespace BL.OnlineStore.Services
{
	public class RegistrationService : IRegistrationService
	{
		
		private readonly IUserSystemService _userSystemService;

		public RegistrationService(IUserSystemService userSystemService)
		{
			_userSystemService = userSystemService;
		}

		public ServiceResult CreateNewUser(RegistrationRequest registrationData)
		{
			
			SystemUserData systemUserData = new SystemUserData
			{
				Login = registrationData.Login,
				OldPassword = registrationData.Password,
				NewPassword = registrationData.Password,
				Email = registrationData.Email,
				Phone = registrationData.Phone,
				FirstName = registrationData.FirstName,
				LastName = registrationData.LastName,
				IdUser =  0,
				Role = (new UserRole(UserRole.RoleEnum.User)).GetRoleName(),
				Status = (new UserStatus(UserStatus.StatusEnum.Active)).GetStatusName()

			};

			var serviceResult = _userSystemService.CreatByRegistration(systemUserData);

			return serviceResult;

		}

		

	}
}
