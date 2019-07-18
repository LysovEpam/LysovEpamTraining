using System;
using System.Collections.Generic;
using System.Text;

namespace BLContracts.Models
{
	public class AuthorizationRequest
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
