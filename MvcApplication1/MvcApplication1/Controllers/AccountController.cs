using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MvcApplication1.DTO;

namespace MvcApplication1.Controllers
{
	[RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
		private AuthRepository _repo = null;

		public AccountController()
		{
			_repo = new AuthRepository();
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(UserRegistrationDto userModel)  // регистрация пользователя 
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await _repo.RegisterUser(userModel); // если все ок регистрируем его

			IHttpActionResult errorResult = GetErrorResult(result);// если нет то ошибка 

			if (errorResult != null)
			{
				return errorResult;
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)// ??  
		{
			if (disposing)
			{
				_repo.Dispose();
			}

			base.Dispose(disposing);
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)// используется для проверки "UserModel" и возвращает правильный код статуса HTTP, если входные данные некорректны.
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// Если  ModelState ошибки не доступны для отправки то просто возвращаем пустой BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
    }
}
