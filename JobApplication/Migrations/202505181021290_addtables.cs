namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        OriginalName = c.String(),
                        FilePath = c.String(),
                        ContentType = c.String(),
                        SizeInBytes = c.Long(nullable: false),
                        UploadedAt = c.DateTime(nullable: false),
                        RelatedEntityId = c.String(),
                        RelatedEntityType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobApplicationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(),
                        InterviewDate = c.DateTime(),
                        Status = c.String(),
                        MadeByUserId = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        JobPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobPosts", t => t.JobPostId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.MadeByUserId)
                .Index(t => t.MadeByUserId)
                .Index(t => t.JobPostId);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDescription = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Status = c.String(),
                        MadeByUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.MadeByUserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.MadeByUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplicationRequests", "MadeByUserId", "dbo.Users");
            DropForeignKey("dbo.JobApplicationRequests", "JobPostId", "dbo.JobPosts");
            DropForeignKey("dbo.JobPosts", "MadeByUserId", "dbo.Users");
            DropForeignKey("dbo.JobPosts", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.JobPosts", new[] { "MadeByUserId" });
            DropIndex("dbo.JobPosts", new[] { "DepartmentId" });
            DropIndex("dbo.JobApplicationRequests", new[] { "JobPostId" });
            DropIndex("dbo.JobApplicationRequests", new[] { "MadeByUserId" });
            DropTable("dbo.JobPosts");
            DropTable("dbo.JobApplicationRequests");
            DropTable("dbo.AppFiles");
        }
    }
}
