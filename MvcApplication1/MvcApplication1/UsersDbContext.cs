using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcApplication1.Poco;


namespace MvcApplication1
{
	public class UsersDbContext : IdentityDbContext<IdentityUser>
	{

		public UsersDbContext() : base("DefaultConnection")
		{
			this.Configuration.ProxyCreationEnabled = false;
			this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Topic> Topics { get; set; }
		public DbSet<Message> Messages { get; set; }
		
	}
}