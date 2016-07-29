using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DTO
{
	public class MessageDto
	{
		public int MessageId { get; set; }
		public string Text { get; set; }
		public int UserEntryId { get; set; }
		public int TopicId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}