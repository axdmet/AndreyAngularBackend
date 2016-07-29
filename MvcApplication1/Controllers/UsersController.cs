using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using MvcApplication1.DTO;
using MvcApplication1.Poco;


namespace MvcApplication1.Controllers 
{
	[RoutePrefix("api/users")]
	public class ValuesController : ApiController
	{
		[HttpGet]
        [Route("GetAll")]
		public IHttpActionResult GetAll(string query = null, int pageSize = 5, int pageIndex=0) // в таком случае можно будеть передать ?query= что-то там
		{
			using (var db = new UsersDbContext())
			{
				var users = string.IsNullOrEmpty(query)
					? db.Users
					: db.Users.Where(u => u.UserName.Contains(query));
				var result = users.OrderBy(u => u.DateCreated).Skip(pageIndex*pageSize).Take(pageSize).ProjectTo<UserDto>().ToList();
				return Json(result);
			}

		}

		[HttpGet]
		[Route("{userId:int}")]
		public IHttpActionResult GetOne(int userId)
		{
			using (var db = new UsersDbContext())
			{
				var user = db.Users.Include(i => i.Roles).FirstOrDefault(u => u.UserEntryId == userId);

				if (user == null)
				{
					return this.NotFound();
				}

				return Json(AutoMapper.Mapper.Map<UserDto>(user));
			}
		}

		[HttpPost]
		[Route("")]
		public IHttpActionResult AddUser(UserEntry user)
		{
			if (user == null)
			{
				return this.BadRequest();
			}
			if (string.IsNullOrEmpty(user.UserName))
			{
				return this.BadRequest("Username is required");
			}

			using (var db = new UsersDbContext())
			{

				if (!string.IsNullOrEmpty(user.UserName))
				{
					if (db.Users.Any(item => item.UserName.ToLower() == user.UserName.ToLower()))
					{
						return this.BadRequest("Username is already exist");
					}
				}

				user.DateCreated = DateTime.Now;

				db.Users.Add(user);

				db.SaveChanges();
			}

			return this.Ok();
		}


		[HttpPut]
		[Route("")]
		public IHttpActionResult UpdateUser(UserEntry user)
		{
			if (user == null)
			{
				return this.BadRequest();
			}

			if (string.IsNullOrEmpty(user.UserName))
			{
				return this.BadRequest("Username is required");
			}

			if (user.UserEntryId == 0)
			{
				return this.BadRequest("UserId must be greater then 0");
			}

			using (var db = new UsersDbContext())
			{

				var current = db.Users.FirstOrDefault(u => u.UserEntryId == user.UserEntryId);

				if (current == null)
				{
					return this.NotFound();
				}

				current.UserName = user.UserName;

				current.DateUpdated = DateTime.Now;

				db.SaveChanges();

				return this.Ok();
			}
		}


		[HttpDelete]
		[Route("{userId:int}")]
		public IHttpActionResult DeleteUser(int userId)
		{
			using (var db = new UsersDbContext())
			{
				var user = db.Users.FirstOrDefault(u => u.UserEntryId == userId);
				if (user == null)
				{
					return this.NotFound();
				}

				db.Users.Remove(user);

				db.SaveChanges();
			}
			
			return this.Ok();
		}

		[HttpPost]
		[Route("{userId:int}/roles")]
		public IHttpActionResult AssignRoles(int userId, int[] roles)
		{
			using (var db = new UsersDbContext())
			{
				// находим юзера по Id
				// Include- выбирает список ролей которые находятся в юзере
				var user = db.Users.Include(i => i.Roles).FirstOrDefault(u => u.UserEntryId == userId);

				if (user == null)
				{
					return this.NotFound();
				}


				// удаляем лишниее
				// в выбраном юзере роли, которые не соотвецтвуют входящим ролям (по Id)  
				var rolesToDelete = user.Roles.Where(r => !roles.Contains(r.RoleId)).ToList();
				foreach (var role in rolesToDelete)
				{
					user.Roles.Remove(role);
				}

				db.SaveChanges();


				// определяем что нужно добавить
				var idsToAdd = roles.Where(r => user.Roles.All(role => role.RoleId != r));

				// находим роли для добавления
				var rolesToAdd = db.Roles.Where(role => idsToAdd.Contains(role.RoleId));


				// добавляем
				foreach (var role in rolesToAdd)
				{
					user.Roles.Add(role);
				}

				db.SaveChanges();
			}

			return this.Ok();
		}
	}
}
