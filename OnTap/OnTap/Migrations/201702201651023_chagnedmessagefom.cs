namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagnedmessagefom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeedMessages", "BarId", "dbo.Bars");
            DropIndex("dbo.FeedMessages", new[] { "BarId" });
            RenameColumn(table: "dbo.FeedMessages", name: "BarId", newName: "Bar_Id");
            AddColumn("dbo.FeedMessages", "FromName", c => c.String());
            AlterColumn("dbo.FeedMessages", "Bar_Id", c => c.Int());
            CreateIndex("dbo.FeedMessages", "Bar_Id");
            AddForeignKey("dbo.FeedMessages", "Bar_Id", "dbo.Bars", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedMessages", "Bar_Id", "dbo.Bars");
            DropIndex("dbo.FeedMessages", new[] { "Bar_Id" });
            AlterColumn("dbo.FeedMessages", "Bar_Id", c => c.Int(nullable: false));
            DropColumn("dbo.FeedMessages", "FromName");
            RenameColumn(table: "dbo.FeedMessages", name: "Bar_Id", newName: "BarId");
            CreateIndex("dbo.FeedMessages", "BarId");
            AddForeignKey("dbo.FeedMessages", "BarId", "dbo.Bars", "Id", cascadeDelete: true);
        }
    }
}
