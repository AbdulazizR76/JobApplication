namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConnectionbetweenApplicationUserAndJobPostJobApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplicationRequests", "MadeByUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.JobPosts", "MadeByUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.JobApplicationRequests", "MadeByUserId");
            CreateIndex("dbo.JobPosts", "MadeByUserId");
            AddForeignKey("dbo.JobPosts", "MadeByUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.JobApplicationRequests", "MadeByUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplicationRequests", "MadeByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "MadeByUserId", "dbo.AspNetUsers");
            DropIndex("dbo.JobPosts", new[] { "MadeByUserId" });
            DropIndex("dbo.JobApplicationRequests", new[] { "MadeByUserId" });
            DropColumn("dbo.JobPosts", "MadeByUserId");
            DropColumn("dbo.JobApplicationRequests", "MadeByUserId");
        }
    }
}
