using System;
using System.Collections.Generic;
using System.Text;

namespace BLContracts.Models
{
	public class OrderSearchRequest
	{
		public string Status { get; set; }
		public string SearchString { get; set; }
	}
}
