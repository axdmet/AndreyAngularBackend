using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcApplication1.Poco
{
	public class Role:AuditableEntity
	{
		[Key]
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public virtual ICollection<UserEntry> UserEntries { get; set; }
	}
}