using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentValidation;
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
			
			var validator = new RoleValidator();
			validator.ValidateAndThrow(role);


			using (var db = new UsersDbContext())
			{
				/*
				if (!string.IsNullOrEmpty(role.RoleName))
				{
					if (db.Roles.Any(item => item.RoleName.ToLower() == role.RoleName.ToLower()))
					{
						return this.BadRequest("Username is already exist");
					}
				}

				role.DateCreated = DateTime.Now;
				db.Roles.Add(role);

				db.SaveChanges();*/
			}

			return this.Ok();
		}


		[HttpPut]
		[Route("")]
		public IHttpActionResult UpdateRole(Role role)
		{
			
			var validator = new RoleValidator();
			validator.ValidateAndThrow(role);


			using (var db = new UsersDbContext())
			{

				var current = db.Roles.FirstOrDefault(u => u.RoleId== role.RoleId);

				if (current == null)
				{
					return this.NotFound();
				}

				current.RoleName = role.RoleName;
				role.DateUpdated = DateTime.Now;

				db.Roles.Add(role);

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
