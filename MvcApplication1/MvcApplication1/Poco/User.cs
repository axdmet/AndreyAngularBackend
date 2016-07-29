using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcApplication1.Poco
{
	[Validator(typeof(UserEntryValidator))]
	public class User : IdentityUser 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual ICollection<Topic> Topics { get; set; }
	}

	public class UserEntryValidator : AbstractValidator<User>
	{
		public UserEntryValidator()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
			
			
		}
	}
}
