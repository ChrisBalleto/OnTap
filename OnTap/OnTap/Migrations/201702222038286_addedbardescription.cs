namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbardescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "BarDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "BarDescription");
        }
    }
}
