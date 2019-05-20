using System;

namespace CommonEntities.Additional
{
	public class UserRole
	{
		public enum RoleEnum
		{
			User,
			Editor,
			Admin
		}

		public RoleEnum Role { get; }

		public UserRole(string role)
		{
			Role = GetRole(role);
		}
		public UserRole(RoleEnum status)
		{
			Role = status;
		}


		public string GetRoleName()
		{
			return GetRoleName(Role);
		}
		
		#region Статические методы получения роли

		public static string GetRoleName(RoleEnum role)
		{
			switch (role)
			{
				case RoleEnum.User: return "User";
				case RoleEnum.Editor: return "Editor";
				case RoleEnum.Admin: return "Admin";
				default: throw new ArgumentOutOfRangeException(nameof(role), role, null);
			}
		}
		public static RoleEnum GetRole(string role)
		{
			switch (role)
			{
				case "User": return RoleEnum.User;
				case "Editor": return RoleEnum.Editor;
				case "Admin": return RoleEnum.Admin;
				default: throw new ArgumentOutOfRangeException(nameof(role), role, null);
			}
		}
		
		#endregion


	}
}
