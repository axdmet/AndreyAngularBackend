using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.DTO
{
	public class TopicDto
	{
	public	int TopicId { get; set; }
    public string Title { get; set; }
	public string CreatedBy { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime? DateUpdated { get; set; }
	}
}