namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bars", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bars", "Rating", c => c.Double());
        }
    }
}
