using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MvcApplication1.DTO;
using MvcApplication1.Poco;


namespace MvcApplication1.Controllers 
{
	[RoutePrefix("api/users")]
	public class ValuesController : ApiController
	{
		[Authorize]
		[HttpGet]
		[Route("getAll")]
		public IHttpActionResult GetAll(string query = null, int pageSize = 5, int pageIndex = 0)
			// в таком случае можно будеть передать ?query= что-то там
		{
			using (var db = new UsersDbContext())
			{
				var users = string.IsNullOrEmpty(query)
					? db.Users
					: db.Users.Where(u => u.UserName.Contains(query));
				var result = users.OrderBy(u => u.).Skip(pageIndex * pageSize).Take(pageSize).ProjectTo<UserDto>().ToList();
				return Json(result);
			}
			return Json("OK");
		}

	}
}
