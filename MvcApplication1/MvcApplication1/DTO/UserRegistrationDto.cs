using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DTO
{
	public class UserRegistrationDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}