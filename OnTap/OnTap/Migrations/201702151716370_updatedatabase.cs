namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "PhoneNumber", c => c.String());
            DropColumn("dbo.Bars", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bars", "LastName", c => c.String());
            DropColumn("dbo.Bars", "PhoneNumber");
        }
    }
}
