namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createddateforreview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Created");
        }
    }
}
