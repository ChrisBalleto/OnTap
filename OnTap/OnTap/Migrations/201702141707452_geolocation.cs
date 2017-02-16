namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geolocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patrons", "Geolocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "Geolocation");
        }
    }
}
