using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using FluentValidation;
using FluentValidation.Attributes;

namespace MvcApplication1.Poco
{
	[Validator (typeof(RoleValidator))]
	public class Role:AuditableEntity
	{
		[Key]
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public virtual ICollection<User> UserEntries { get; set; }
	}

	public class RoleValidator : AbstractValidator<Role>
	{
		public RoleValidator()
		{
			RuleFor(x => x.RoleName).NotEmpty();
		}
	}
}