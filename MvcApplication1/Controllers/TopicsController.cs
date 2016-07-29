using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper.QueryableExtensions;
using MvcApplication1;
using MvcApplication1.DTO;
using MvcApplication1.Poco;

namespace MvcApplication1.Controllers
{
	[RoutePrefix("api/topics")]
	public class TopicsController : ApiController
	{

		[HttpGet]
        [Route("GetAll")]
		public IHttpActionResult GetAll(string query = null,int pageSize=50,int pageIndex=0 )
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

		[HttpPost]
        [Route("AddTopic")]
		public IHttpActionResult AddTopic(Topic topic)
		{
			if (topic == null)
			{
				return this.BadRequest();
			}
			if (string.IsNullOrEmpty(topic.Title))
			{
				return this.BadRequest("Topic title is required");
			}

			using (var db = new UsersDbContext())
			{
				if (db.Topics.Any(item => item.Title.ToLower() == topic.Title.ToLower()))
				{
					return this.BadRequest("Topic title is already exist");
				}

                topic.UserEntryId = 1;

				topic.DateCreated = DateTime.Now;

				db.Topics.Add(topic);

				db.SaveChanges();
			}

			return this.Ok();
		}

	


		[HttpPut]
        [Route("UpdateTopic")]
		public IHttpActionResult UpdateTopic(Topic topic)
		{
			if (topic == null)
			{
				return this.BadRequest();
			}

			if (string.IsNullOrEmpty(topic.Title))
			{
				return this.BadRequest("Topic title is required");
			}

			if (topic.TopicId== 0)
			{
				return this.BadRequest("TopicId must be greater then 0");
			}

			using (var db = new UsersDbContext())
			{

				var current = db.Topics.FirstOrDefault(u => u.TopicId == topic.TopicId);

				if (current == null)
				{
					return this.NotFound();
				}

				current.Title = topic.Title;
				topic.DateUpdated = DateTime.Now;
				//db.Topics.updat(topic);

				db.SaveChanges();

				return this.Ok();
			}
		}


		[HttpDelete]
        [Route("DeleteTopic/{topicId:int}")]
		public IHttpActionResult DeleteTopic(int topicId)
		{
			using (var db = new UsersDbContext())
			{
				var topic = db.Topics.FirstOrDefault(u => u.TopicId== topicId);
				if (topic == null)
				{
					return this.NotFound();
				}

				db.Topics.Remove(topic);

				db.SaveChanges();
			}

			return this.Ok();
		}


	
	}
}

        