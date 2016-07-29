using System.Data.Entity;
using MvcApplication1.Poco;


namespace MvcApplication1
{
	public class UsersDbContext: DbContext
	{

		public UsersDbContext() : base("DefaultConnection")
		{
			this.Configuration.ProxyCreationEnabled = false;
			this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<UserEntry> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Topic> Topics { get; set; }
        public DbSet<Message> Messages { get; set; }
	}
}