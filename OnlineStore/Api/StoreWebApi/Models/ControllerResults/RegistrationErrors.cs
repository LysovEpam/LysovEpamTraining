namespace StoreWebApi.Models.ControllerResults
{
	public class RegistrationErrors
	{
		public string FirstNameError { get; set; }
		public string LastNameError { get; set; }
		public string EmailError { get; set; }
		public string PhoneError { get; set; }
		public string LoginError { get; set; }
		public string PasswordError { get; set; }
	}
}
