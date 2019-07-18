using System;

namespace CommonEntities.Additional
{
	public class UserRole
	{
		public const string RoleUser = "User";
		public const string RoleEditor = "Editor";
		public const string RoleAdmin = "Admin";
		

		public enum RoleEnum
		{
			User = 10,
			Editor = 11,
			Admin = 12
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
		
		
		public static string GetRoleName(RoleEnum role)
		{
			switch (role)
			{
				case RoleEnum.User: return RoleUser;
				case RoleEnum.Editor: return RoleEditor;
				case RoleEnum.Admin: return RoleAdmin;
				default: throw new ArgumentOutOfRangeException(nameof(role), role, null);
			}
		}
		public static RoleEnum GetRole(string role)
		{
			switch (role)
			{
				case RoleUser: return RoleEnum.User;
				case RoleEditor: return RoleEnum.Editor;
				case RoleAdmin: return RoleEnum.Admin;
				default: throw new ArgumentOutOfRangeException(nameof(role), role, null);
			}
		}

		

	}
}
