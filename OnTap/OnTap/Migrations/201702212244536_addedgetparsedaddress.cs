namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgetparsedaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "ParsedAddress", c => c.String());
            AddColumn("dbo.Patrons", "ParsedAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "ParsedAddress");
            DropColumn("dbo.Bars", "ParsedAddress");
        }
    }
}
