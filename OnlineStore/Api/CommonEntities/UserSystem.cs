namespace CommonEntities
{
	public class UserSystem : BaseDbEntity
	{
		public string FirsName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int UserAdmittanceId { get; set; }

		public UserAdmittance UserAdmittance { get; set; }

		
		#region Конструктор

		public UserSystem()
		{

		}
		public UserSystem(int userId, string firsName, string lastName, string email, string phone, int userAdmittanceId) : base(userId)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAdmittanceId = userAdmittanceId;

			UserAdmittance = null;

		}
		public UserSystem(string firsName, string lastName, string email, string phone, int userAdmittanceId) 
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			UserAdmittanceId = userAdmittanceId;

			UserAdmittance = null;

		}
		public UserSystem(int userId, string firsName, string lastName, string email, string phone, UserAdmittance userAdmittance) : base(userId)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			if (userAdmittance.IdEntity != null)
				UserAdmittanceId = userAdmittance.IdEntity.Value;

			UserAdmittance = userAdmittance;
	
		}
		public UserSystem(string firsName, string lastName, string email, string phone, UserAdmittance userAdmittance)
		{
			FirsName = firsName;
			LastName = lastName;
			Email = email;
			Phone = phone;
			if (userAdmittance?.IdEntity != null)
				UserAdmittanceId = userAdmittance.IdEntity.Value;

			UserAdmittance = userAdmittance;
		}

		#endregion

		
	}
}
