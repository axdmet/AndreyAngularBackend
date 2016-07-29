using System;

namespace MvcApplication1.DTO
{
	public class RoleDto
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
	}
}