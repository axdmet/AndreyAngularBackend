using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcApplication1.Poco
{
	[Validator(typeof(TopicVilidator))]
	public class Topic:AuditableEntity
	{
		public int TopicId { get; set; }
		public string Title { get; set; }
		public string UserId { get; set; }
		public IdentityUser User { get; set; }
		public ICollection<Message> Messages { get; set; }
	}

	public class TopicVilidator : AbstractValidator<Topic>
	{
		public TopicVilidator()
		{
			RuleFor(x => x.Title).NotEmpty();
			RuleFor(x => x.UserId).NotNull();
		}
	}
	
}