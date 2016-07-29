using MvcApplication1.Poco;


namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<UsersDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
			
        }

		protected override void Seed(UsersDbContext context)
		{
			if (context.Users.Count() < 10)
			{
				for (int i = 0; i < 10; i++)
				{
					context.Roles.Add(new Role()
					{
						RoleName = "Role_" + i
					});
				}

				context.SaveChanges();

				var dbRoles = context.Roles.ToList();
				var dbTopics = context.Topics.ToList();


				for (int i = 0; i < 10; i++)
				{
					context.Users.Add(new UserEntry()
					{
						UserName = "Alex_" + i,
						FirstName = "Fisrt Name " + i,
						LastName = "Last Name " + i,
						DateCreated = DateTime.Now,
						Roles = dbRoles,
						Topics = dbTopics
						
					});
				}
				context.SaveChanges();

				

			}

			var topicCreator = context.Users.First();

			if (!context.Topics.Any())
			{
				for (int i = 0; i < 10; i++)
				{
					context.Topics.Add(new Topic()
					{
						Title = "Title_" + i,
						DateCreated = DateTime.Now,
						UserEntryId = topicCreator.UserEntryId
					});
				}
				context.SaveChanges(); // ну как видим топики то там есть)
			}
		}
    }
}
