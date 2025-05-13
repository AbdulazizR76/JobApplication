namespace JobApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Position");
        }
    }
}
