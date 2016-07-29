using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.Poco;

namespace MvcApplication1.Controllers
{
	[RoutePrefix("api/roles")]
	
    public class RolesController : ApiController
    {
			
		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAll(string query = null) 
		{
			using (var db = new UsersDbContext())
			{
				var roles = string.IsNullOrEmpty(query)
					? db.Users
					: db.Users.Where(u => u.UserName.Contains(query));
			
				return Json(roles);
			}

		}

		[HttpGet]
		[Route("{roleId:int}")]
		public IHttpActionResult GetOne(int roleId)
		{
			using (var db = new UsersDbContext())
			{
				var role = db.Roles.FirstOrDefault(u => u.RoleId == roleId);

				if (role == null)
				{
					return this.NotFound();
				}

				return Json(role);
			}
		}

		[HttpPost]
		[Route("")]
		public IHttpActionResult AddRole(Role role)
		{
			if (role == null)
			{
				return this.BadRequest();
			}
			if (string.IsNullOrEmpty(role.RoleName))
			{
				return this.BadRequest("Username is required");
			}

			using (var db = new UsersDbContext())
			{

				if (!string.IsNullOrEmpty(role.RoleName))
				{
					if (db.Roles.Any(item => item.RoleName.ToLower() == role.RoleName.ToLower()))
					{
						return this.BadRequest("Username is already exist");
					}
				}

				
				db.Roles.Add(role);

				db.SaveChanges();
			}

			return this.Ok();
		}


		[HttpPut]
		[Route("")]
		public IHttpActionResult UpdateRole(Role role)
		{
			if (role == null)
			{
				return this.BadRequest();
			}

			if (string.IsNullOrEmpty(role.RoleName))
			{
				return this.BadRequest("Username is required");
			}

			if (role.RoleId== 0)
			{
				return this.BadRequest("UserId must be greater then 0");
			}

			using (var db = new UsersDbContext())
			{

				var current = db.Roles.FirstOrDefault(u => u.RoleId== role.RoleId);

				if (current == null)
				{
					return this.NotFound();
				}

				current.RoleName = role.RoleName;

				

				db.SaveChanges();

				return this.Ok();
			}
		}


		[HttpDelete]
		[Route("{userId:int}")]
		public IHttpActionResult DeleteRole(int roleId)
		{
			using (var db = new UsersDbContext())
			{
				var role = db.Roles.FirstOrDefault(u => u.RoleId == roleId);
				if (role == null)
				{
					return this.NotFound();
				}

				db.Roles.Remove(role);

				db.SaveChanges();
			}
			
			return this.Ok();
		}

    }
}
