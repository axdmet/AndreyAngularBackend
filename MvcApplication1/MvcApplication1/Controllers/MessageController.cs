using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MvcApplication1.DTO;
using MvcApplication1.Poco;
using System.Web.Http.Validation;

namespace MvcApplication1.Controllers
{
	[RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {
		[HttpGet]
		[Route("{topicId:int}")]
		public IHttpActionResult GetAll(int topicId,  string query = null, int pageSize = 5, int pageIndex = 0)
		{
			using (var db = new UsersDbContext())
			{

			var	messages=db.Messages.Where(u => u.TopicId == topicId);
				//var topics = string.IsNullOrEmpty(query)
				//	? db.Messages
				//	: db.Messages.Where(u => u.Text.Contains(query));
				//var res = topics.OrderBy(u => u.DateCreated).Skip(pageIndex * pageSize).Take(pageSize).ProjectTo<MessageDto>().ToList();

				return Json(messages);
			}

		}

		[HttpGet]
		[Route("{message:int}")]
		public IHttpActionResult GetOne(int messageId)
		{
			using (var db = new UsersDbContext())
			{
				var message = db.Messages.FirstOrDefault(u => u.MessageId == messageId);

				if (message == null)
				{
					return this.NotFound();
				}

				return Json(message);
			}
		}

		//[HttpPost]
		//[Route("")]
		//public IHttpActionResult AddMessage(Message message)
		//{

		//	var validator = new MessageValidator();
		//	validator.ValidateAndThrow(message);


		//	using (var db = new UsersDbContext())
		//	{
		//		var userId = db.Users.FirstOrDefault(t => t.UserEntryId == message.UserEntryId);
		//		if (userId == null)
		//		{
		//			return this.BadRequest("This UserEntryId does not exist in the database");
		//		}

		//		message.DateCreated = DateTime.Now;

		//		db.Messages.Add(message);

		//		db.SaveChanges();
		//	}

		//	return this.Ok();
		//}

		//[HttpPost]
		//[Route("")]
		//public IHttpActionResult UpdateMessage(Message message)
		//{

		//	var validator = new MessageValidator();
		//	validator.ValidateAndThrow(message);


		//	using (var db=new UsersDbContext())
		//	{
		//		var current = db.Messages.FirstOrDefault(u => u.MessageId == message.MessageId);
		//		current.Text = message.Text;
		//		message.DateUpdated = DateTime.Now;

		//		var userId = db.Users.FirstOrDefault(t => t.UserEntryId == message.UserEntryId);
		//		if (userId == null)
		//		{
		//			return this.BadRequest("This UserEntryId does not exist in the database");
		//		}
		//		db.Messages.Add(message);
		//		db.SaveChanges();
		//	}
		//	return this.Ok();
		//}

		//[HttpDelete]
		//[Route("{message:int}")]
		//public IHttpActionResult DeleteTopic(int messageId)
		//{
		//	using (var db = new UsersDbContext())
		//	{
		//		var message = db.Messages.FirstOrDefault(u => u.MessageId== messageId);
		//		if (message == null)
		//		{
		//			return this.NotFound();
		//		}

		//		db.Messages.Remove(message);

		//		db.SaveChanges();
		//	}

		//	return this.Ok();
		//}

		[Authorize]
		[HttpGet]
		[Route("my")]
		public IHttpActionResult GetMy()
		{
			using (var db = new UsersDbContext())
			{

				var messages = db.Messages.FirstOrDefault(u => u.User.UserName == HttpContext.Current.User.ToString());
				if (messages == null)
				{
					return this.NotFound();
				}
				return Json(messages);
			}

		}

    }
}
