using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http.Validation;
using System.Web.Providers.Entities;
using FluentValidation;
using FluentValidation.Attributes;

namespace MvcApplication1.Poco
{
    [Validator (typeof(MessageValidator))]
	public class Message : AuditableEntity
	{
		public int MessageId { get; set; }
		public string  Text { get; set; }
		public int UserEntryId { get; set; }
		public int TopicId { get; set; }
		public UserEntry UserEntry { get; set; }
		public Topic Topic { get; set; }
	}
	public class MessageValidator : AbstractValidator<Message>
	{
		public MessageValidator()
		{
			RuleFor(x => x.Text).NotEmpty();
			RuleFor(x => x.UserEntryId).NotNull();
			RuleFor(x => x.TopicId).NotNull();
		}
	}
}