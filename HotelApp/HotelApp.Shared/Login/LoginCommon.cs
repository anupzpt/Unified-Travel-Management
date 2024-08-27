using HotelApp.Shared.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Login
{
	public class LoginCommon
	{
		public string Code { get; set; }
		public string Flag { get; set; }
		public string Username { get; set; }
		public string FullName { get; set; }
		public string ContactNo { get; set; }
		public string EmailId { get; set; }
		public string IsAdmin { get; set; }
		public string IsSupperUser { get; set; }
		public string UserPassword { get; set; }
		public string KeepLoggedIn { get; set; }

	}
}
