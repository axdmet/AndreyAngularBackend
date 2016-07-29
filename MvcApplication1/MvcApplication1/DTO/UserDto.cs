using System;
using System.Collections.Generic;
using MvcApplication1.Poco;

namespace MvcApplication1.DTO
{
	public class UserDto
	{
		public int UserEntryId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public virtual ICollection<RoleDto> Roles { get; set; }
	}
}