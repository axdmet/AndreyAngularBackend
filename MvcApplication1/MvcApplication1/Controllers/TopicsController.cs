using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MvcApplication1.DTO;
using MvcApplication1.Poco;

namespace MvcApplication1.Controllers
{
	[RoutePrefix("api/topics")]
	public class TopicsController : ApiController
	{

		[HttpGet]
		[Route("")]
		public IHttpActionResult GetAll(string query = null,int pageSize=5,int pageIndex=0 )
		{
			using (var db = new UsersDbContext())
			{
				var topics = string.IsNullOrEmpty(query)
					? db.Topics
					: db.Topics.Where(u => u.Title.Contains(query));
				var res = topics.OrderBy(u => u.DateCreated).Skip(pageIndex*pageSize).Take(pageSize).ProjectTo<TopicDto>().ToList();

				return Json(res);
			}

		}

		[HttpGet]
		[Route("{topicId:int}")]
		public IHttpActionResult GetOne(int topicId)
		{
			using (var db = new UsersDbContext())
			{
				var topic = db.Topics.FirstOrDefault(u => u.TopicId == topicId);

				if (topic == null)
				{
					return this.NotFound();
				}

				return Json(topic);
			}
		}
		[Authorize]
		[HttpPost]
		[Route("")]
		public IHttpActionResult AddTopic(Topic topic)
		{

			var validator = new TopicVilidator();
			validator.ValidateAndThrow(topic);

			
			using (var db = new UsersDbContext())
			{
				var currentUser = db.Users.FirstOrDefault(u => u.UserName == HttpContext.Current.User.ToString());

				if (db.Topics.Any(item => item.Title.ToLower() == topic.Title.ToLower()))
				{
					return this.BadRequest("Topic title is already exist");
				}

				topic.DateCreated = DateTime.Now;
				topic.UserId = currentUser.Id;

				db.Topics.Add(topic);

				db.SaveChanges();
			}

			return this.Ok();
		}
		[Authorize]
		[HttpGet]
		[Route("my")]
		public IHttpActionResult GetMy()
		{
			using (var db = new UsersDbContext())
			{

				var topics = db.Topics.FirstOrDefault(u => u.User.UserName == HttpContext.Current.User.ToString());
				if (topics == null)
				{
					return this.NotFound();
				}
				return Json(topics);
			}
			
		}


		//[HttpPut]
		//[Route("")]
		//public IHttpActionResult UpdateTopic(Topic topic)
		//{
			
		//	var validator = new TopicVilidator();
		//	validator.ValidateAndThrow(topic);


		//	using (var db = new UsersDbContext())
		//	{

		//		var current = db.Topics.FirstOrDefault(u => u.TopicId == topic.TopicId);

		//		if (current == null)
		//		{
		//			return this.NotFound();
		//		}

		//		current.Title = topic.Title;
		//		topic.DateUpdated = DateTime.Now;
		//		var userId = db.Users.FirstOrDefault(t => t.UserEntryId == topic.UserEntryId);
		//		if (userId==null)
		//		{
		//			return this.BadRequest("This UserEntryId does not exist in the database");
		//		}
		//		db.Topics.Add(topic);

		//		db.SaveChanges();

		//		return this.Ok();
		//	}
		//}


		//[HttpDelete]
		//[Route("{topicId:int}")]
		//public IHttpActionResult DeleteTopic(int topicId)
		//{
		//	using (var db = new UsersDbContext())
		//	{
		//		var topic = db.Topics.FirstOrDefault(u => u.TopicId== topicId);
		//		if (topic == null)
		//		{
		//			return this.NotFound();
		//		}

		//		db.Topics.Remove(topic);

		//		db.SaveChanges();
		//	}

		//	return this.Ok();
		//}


	
	}
}

        