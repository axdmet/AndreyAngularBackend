using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Poco
{
	public class Topic:AuditableEntity
	{
		public int TopicId { get; set; }
		public string Title { get; set; }
		public int UserEntryId { get; set; }
		public UserEntry UserEntry { get; set; }
	}

	
}