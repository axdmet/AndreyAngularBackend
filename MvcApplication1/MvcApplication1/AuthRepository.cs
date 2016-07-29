using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcApplication1.DTO;
using MvcApplication1.Poco;

namespace MvcApplication1
{
	public class AuthRepository:IDisposable
	{
		private UsersDbContext _ctx;

		private UserManager<IdentityUser> _userManager; // UserManager знает когда и как хешировать пароль и валидировать пользователя

		public AuthRepository()
		{
			_ctx = new UsersDbContext();
			_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));  // передаем UserManager контекст нашего юзера
		}

		public async Task<IdentityResult> RegisterUser(UserRegistrationDto userModel)  //  регистрирует юзера 
		{
			IdentityUser user = new IdentityUser
			{
				UserName = userModel.UserName
				
			};

			var result = await _userManager.CreateAsync(user, userModel.Password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password) // находит зарегистрированого юзера 
		{
			IdentityUser user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose() 
		{
			_ctx.Dispose();
			_userManager.Dispose();

		}
	}
}