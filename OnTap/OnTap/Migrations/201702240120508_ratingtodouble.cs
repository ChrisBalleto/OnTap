namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingtodouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bars", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bars", "Rating", c => c.Int());
        }
    }
}
