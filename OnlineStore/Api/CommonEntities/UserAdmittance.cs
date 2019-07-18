using CommonEntities.Additional;

namespace CommonEntities
{
	public class UserAdmittance : BaseDbEntity
	{

		
		#region Свойства класса

		public string Login { get; set; }
		public string PasswordHash { get; set; }
		//public string Status { get; set; }
		//public string Role { get; set; }

		public UserStatus UserStatus { get; set; }
		public UserRole UserRole { get; set; }


		#endregion

		#region Конструктор

		public UserAdmittance()
		{

		}
		public UserAdmittance(int userAccessId, string login, string passwordHash, string status, string role) : base(userAccessId)
		{
			Login = login;
			PasswordHash = passwordHash;
			//Status = status;
			//Role = role;

			UserStatus = new UserStatus(status);
			UserRole = new UserRole(role);
		}
		public UserAdmittance(int userAccessId, string login, string passwordHash, UserStatus status, UserRole role) : base(userAccessId)
		{
			Login = login;
			PasswordHash = passwordHash;
			//Status = status.GetStatusName();
			//Role = role.GetRoleName();

			UserStatus = status;
			UserRole = role;
		}
		public UserAdmittance(string login, string passwordHash, UserStatus status, UserRole role)
		{
			Login = login;
			PasswordHash = passwordHash;
			//Status = status.GetStatusName();
			//Role = role.GetRoleName();

			UserStatus = status;
			UserRole = role;
		}

		#endregion

		
	}
}
