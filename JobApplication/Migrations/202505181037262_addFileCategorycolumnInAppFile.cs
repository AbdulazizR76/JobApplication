namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileCategorycolumnInAppFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppFiles", "FileCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppFiles", "FileCategory");
        }
    }
}
