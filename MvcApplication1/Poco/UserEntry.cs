using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Poco
{
	public class UserEntry:AuditableEntity
	{
		[Key]
		public int UserEntryId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		public virtual ICollection<Topic> Topics { get; set; }
	}
}
