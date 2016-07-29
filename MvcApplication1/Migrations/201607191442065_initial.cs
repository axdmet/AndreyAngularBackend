namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserEntries",
                c => new
                    {
                        UserEntryId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserEntryId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UserEntryId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.UserEntries", t => t.UserEntryId, cascadeDelete: true)
                .Index(t => t.UserEntryId);
            
            CreateTable(
                "dbo.UserEntryRoles",
                c => new
                    {
                        UserEntry_UserEntryId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserEntry_UserEntryId, t.Role_RoleId })
                .ForeignKey("dbo.UserEntries", t => t.UserEntry_UserEntryId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.UserEntry_UserEntryId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "UserEntryId", "dbo.UserEntries");
            DropForeignKey("dbo.UserEntryRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserEntryRoles", "UserEntry_UserEntryId", "dbo.UserEntries");
            DropIndex("dbo.UserEntryRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserEntryRoles", new[] { "UserEntry_UserEntryId" });
            DropIndex("dbo.Topics", new[] { "UserEntryId" });
            DropTable("dbo.UserEntryRoles");
            DropTable("dbo.Topics");
            DropTable("dbo.UserEntries");
            DropTable("dbo.Roles");
        }
    }
}
