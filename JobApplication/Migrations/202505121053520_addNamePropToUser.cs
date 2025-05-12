namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNamePropToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
