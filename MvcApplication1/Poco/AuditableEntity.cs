using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Poco
{
	public abstract class AuditableEntity
	{
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}