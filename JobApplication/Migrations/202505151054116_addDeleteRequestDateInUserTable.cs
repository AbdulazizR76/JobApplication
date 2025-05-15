namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeleteRequestDateInUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DeletionRequestedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletionRequestedDate");
        }
    }
}
