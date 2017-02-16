namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class over21tojoin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patrons", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "BirthDate");
        }
    }
}
